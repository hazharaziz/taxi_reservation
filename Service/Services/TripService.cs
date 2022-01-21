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
        private List<Trip> _trips;
        private readonly UserService _userService;
        private readonly DriverService _driverService;
        private readonly CreditService _creditService;
        private ICalculateStrategy _calculationStrategy;
        private long _tripCounter;

        public TripService(UserService userService, DriverService driverService, CreditService creditService)
        {
            _trips = new List<Trip>();
            _userService = userService;
            _driverService = driverService;
            _creditService = creditService;
            _calculationStrategy = new AfternoonCalculation();
            _tripCounter = 1;
        }

        public List<Trip> GetAllTrips()
        {
            return _trips;
        }

        public Trip GetTripById(long tripId)
        {
            return _trips.FirstOrDefault(t => t.Id == tripId);
        }

        public Trip RequestTrip(long passengerId, Address origin, Address destination)
        {
            var passenger = _userService.GetPassengerById(passengerId);

            if (passenger == null)
                throw new Exception("Passenger does not exist");

            var trip = new Trip()
            {
                Id = _tripCounter++,
                Origin = origin,
                Destination = destination,
                Passenger = passenger
            };

            trip.Price = _calculationStrategy.CalculatePrice(origin, destination);
            trip.Status = TripStatus.Pending;

            Console.WriteLine($"Passenger {passenger.Name} requested a trip from " +
                $"{origin.Description} to {destination.Description}");

            _trips.Add(trip);
            _driverService.NotifyAllDrivers(trip);

            return trip;
        }

        public void PayTripPrice(long tripId, long passengerId)
        {
            var trip = GetTripById(tripId);
            var passenger = _userService.GetPassengerById(passengerId);

            if (trip == null)
                throw new Exception("Trip does not exist");

            if (passenger == null)
                throw new Exception("Passenger does not exist");

            if (trip.Passenger.Id != passenger.Id)
                throw new Exception("This passenger is not allowed to pay this trip's price");

            if (trip.Options.PaymentType == PaymentType.Cash)
                return;

            if (passenger.Balance < trip.Price)
            {
                _creditService.Deposit(passenger.Id, trip.Price);
            }

            passenger.Balance -= trip.Price;
            trip.Driver.Balance += trip.Price;
            trip.Status = TripStatus.PaidByCard;
        }

        public void EndTrip(long tripId, long driverId)
        {
            var trip = GetTripById(tripId);
            var driver = _userService.GetDriverById(driverId);

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
            var user = _userService.GetUserById(userId);

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
            _calculationStrategy = newCalculationStrategy;
        }

        public Trip AssignDriverToTrip(long tripId, long driverId)
        {
            var trip = _trips.FirstOrDefault(t => t.Id == tripId);
            var driver = _userService.GetDriverById(driverId);

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
