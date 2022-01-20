using Domain.Entities;
using Domain.Enums;
using Repository.Interfaces;
using Service.CalculationStrategies;
using Service.Interfaces;

namespace Service.Managers
{
    public class TripManager
    {
        private readonly DriverManager _driverManager;
        private ICalculateStrategy _calculationStrategy;

        public TripManager(DriverManager driverMananger)
        {
            _driverManager = driverMananger;
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
