using Domain.Entities;
using Domain.Enums;
using Service.CalculationStrategies;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Services
{
    public class TripService
    {
        private List<Trip> trips;
        private readonly UserService userService;
        private readonly DriverService driverService;
        private readonly CreditService creditService;
        private ICalculateStrategy calculationStrategy;
        private long tripCounter;

        public TripService(UserService userService, DriverService driverService, CreditService creditService)
        {
            trips = new List<Trip>();
            this.userService = userService;
            this.driverService = driverService;
            this.creditService = creditService;
            calculationStrategy = new AfternoonCalculationStrategy();
            tripCounter = 1;
        }

        public List<Trip> GetAllTrips()
        {
            return trips;
        }

        public Trip GetTripById(long tripId)
        {
            return trips.FirstOrDefault(t => t.Id == tripId);
        }

        public Trip RequestTrip(long passengerId, Address origin, Address destination)
        {
            var passenger = userService.GetPassengerById(passengerId);

            if (passenger == null)
                throw new Exception("Passenger does not exist");

            var trip = new Trip()
            {
                Id = tripCounter++,
                Origin = origin,
                Destination = destination,
                Passenger = passenger,
                Options = new TripOptions()
                {
                    PaymentType = PaymentType.Card
                }
            };

            trip.Price = calculationStrategy.CalculatePrice(origin, destination);
            trip.Status = TripStatus.Pending;

            Console.WriteLine($"Passenger {passenger.Name} requested a trip from " +
                $"{origin.Description} to {destination.Description}");

            trips.Add(trip);
            driverService.NotifyAllDrivers(trip);

            return trip;
        }

        public void PayTripPrice(long tripId, long passengerId)
        {
            var trip = GetTripById(tripId);
            var passenger = userService.GetPassengerById(passengerId);

            if (trip == null)
                throw new Exception("Trip does not exist");

            if (passenger == null)
                throw new Exception("Passenger does not exist");

            if (trip.Passenger.Id != passenger.Id)
                throw new Exception("This passenger is not allowed to pay this trip's price");

            if (trip.Status == TripStatus.Canceled)
                throw new Exception("Trip is canceled!");

            if (trip.Options.PaymentType == PaymentType.Cash)
                return;

            if (passenger.Balance < trip.Price)
            {
                creditService.Deposit(passenger.Id, trip.Price);
            }

            passenger.Balance -= trip.Price;
            trip.Driver.Balance += trip.Price;
            trip.Status = TripStatus.PaidByCard;
        }

        public void EndTrip(long tripId, long driverId)
        {
            var trip = GetTripById(tripId);
            var driver = userService.GetDriverById(driverId);

            if (trip == null)
                throw new Exception("Trip does not exist");

            if (driver == null)
                throw new Exception("Driver does not exist");

            if (trip.Driver.Id != driver.Id)
                throw new Exception("This driver is not allowed to end this trip");

            if (trip.Status != TripStatus.PaidByCard &&
                trip.Options.PaymentType == PaymentType.Card)
                throw new Exception("Trip must be settled first!");

            trip.Status = TripStatus.Ended;

            Console.WriteLine($"Driver {trip.Driver.Name} ended the trip!");
        }

        public void CancelTrip(long tripId, long userId)
        {
            var trip = GetTripById(tripId);
            var user = userService.GetUserById(userId);

            if (trip == null)
                throw new Exception("Trip does not exist");

            if (user == null)
                throw new Exception("User does not exist");

            if (trip.Passenger.Id != user.Id || trip.Driver.Id != user.Id)
                throw new Exception($"This user is not allowed to cancel this trip {trip.Id}");

            trip.Status = TripStatus.Canceled;

            Console.WriteLine($"{user.Name} canceled the trip!");
        }

        public void SetCalculationStrategy(ICalculateStrategy newCalculationStrategy)
        {
            calculationStrategy = newCalculationStrategy;
        }

        public Trip AssignDriverToTrip(long tripId, long driverId)
        {
            var trip = trips.FirstOrDefault(t => t.Id == tripId);
            var driver = userService.GetDriverById(driverId);

            if (trip == null)
                throw new Exception("Trip does not exists");

            if (driver == null)
                throw new Exception("Driver does not exist");

            trip.Status = TripStatus.OnTheWay;
            trip.Driver = driver;

            Console.WriteLine($"Driver {driver.Name} is assigned to trip id {trip.Id}");

            return trip;
        }
    }
}
