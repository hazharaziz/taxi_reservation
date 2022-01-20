using Domain.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Managers
{
    public class DriverManager
    {
        private readonly IRepository<Driver> _driverRepository;

        public DriverManager(IRepository<Driver> driverRepository)
        {
            _driverRepository = driverRepository;
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
            var closestDrivers = _driverRepository.GetAll()
                .OrderBy(d => d.CurrentLocation.CalculateDistance(originAddress))
                .Take(10)
                .ToList();

            return closestDrivers;
        }
    }
}
