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
        private readonly DriverService _driverManager;
        private ICalculateStrategy _calculationStrategy;
        private long _tripCounter;

        public TripService(UserService userService, DriverService driverMananger)
        {
            _trips = new List<Trip>();
            _userService = userService;
            _driverManager = driverMananger;
            _calculationStrategy = new AfternoonCalculation();
            _tripCounter = 1;
        }

        public List<Trip> GetAllTrips()
        {
            return _trips;
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
            _driverManager.NotifyAllDrivers(trip);

            return trip;
        }

        public bool EndTrip(Trip trip)
        {

            if (trip.Options.PaymentType == PaymentType.Card)
            {
                if (trip.Passenger.Balance < trip.Price)
                {
                    return false;
                }
                else
                {
                    trip.Passenger.Balance -= trip.Price;
                    trip.Driver.Balance += trip.Price;
                }
            }

            trip.Status = TripStatus.Ended;

            Console.WriteLine($"Driver {trip.Driver.Name} ended the trip!");

            return true;
        }

        public bool CancelTrip(long userId, Trip trip)
        {
            var user = _userService.GetUserById(userId);

            if (user == null)
                throw new Exception("User does not exist");

            trip.Status = TripStatus.Canceled;

            Console.WriteLine($"{user.Name} canceled the trip!");

            return true;
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
