using Domain.Entities;
using Domain.Enums;
using Repository.Interfaces;
using Service.CalculationStrategies;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Managers
{
    public class TripManager
    {
        private readonly IRepository<Trip> _tripRepository;
        private readonly DriverManager _driverManager;
        private ICalculateStrategy _calculationStrategy;

        public TripManager(IRepository<Trip> tripRepository, DriverManager driverManager)
        {
            _tripRepository = tripRepository;
            _driverManager = driverManager;
            _calculationStrategy = new AfternoonCalculation();
        }

        public Trip RequestTrip(Passenger passenger, Address origin, Address destination)
        {
            var trip = new Trip()
            {
                Origin = origin,
                Destination = destination,
                Passenger = passenger
            };

            trip.Price = _calculationStrategy.CalculatePrice(origin, destination);
            var driver = _driverManager.FindDriver(trip);

            if (driver != null)
            {
                trip.Status = TripStatus.OnTheWay;
            }

            return trip;
        }

        public bool EndTrip(Trip trip)
        {
            trip.Status = TripStatus.Ended;
            return true;
        }

        public bool CancelTrip(Trip trip)
        {
            trip.Status = TripStatus.Canceled;
            return true;
        }

        public void SetCalculationStrategy(ICalculateStrategy newCalculationStrategy)
        {
            _calculationStrategy = newCalculationStrategy;
        }
    }
}
