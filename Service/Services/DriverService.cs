using Domain.Entities;
using Domain.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Service.Services
{
    public class DriverService
    {
        private List<Driver> _drivers;

        public DriverService()
        {
            _drivers = new List<Driver>()
            {
                new Driver()
                {
                    Id = 1,
                    Car = CarType.BlueNisan,
                    CurrentLocation = new Address()
                    {
                        Latitude = 10,
                        Longitude = 20,
                        Description = "New York"
                    }
                },
                new Driver()
                {
                    Id = 3,
                    Car = CarType.Peikan,
                    CurrentLocation = new Address()
                    {
                        Latitude = 20,
                        Longitude = 40,
                        Description = "Los Angles"
                    }
                }
            };
        }

        public Driver FindDriver(Trip trip)
        {
            var closestDrivers = FindClosestDrivers(trip.Origin);

            foreach (var driver in closestDrivers)
            {
                var response = driver.RespondTripRequest(trip);
                if (response)
                    return driver;
            }

            return null;
        }

        private List<Driver> FindClosestDrivers(Address originAddress)
        {
            var closestDrivers = _drivers
                .OrderBy(d => d.CurrentLocation.CalculateDistance(originAddress))
                .Take(10)
                .ToList();

            return closestDrivers;
        }
    }
}
