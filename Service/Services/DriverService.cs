using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Services
{
    public class DriverService
    {
        private readonly UserService _userService;

        public DriverService(UserService userService)
        {
            _userService = userService;
        }

        public void NotifyAllDrivers(Trip trip)
        {
            var closestDrivers = FindClosestDrivers(trip.Origin);

            Console.WriteLine("Notify closest drivers about this trip");

            foreach (var driver in closestDrivers)
            {
                Console.WriteLine($"Driver {driver.Name} notified!");
            }
        }

        private List<Driver> FindClosestDrivers(Address originAddress)
        {
            Console.WriteLine("Finding closest drivers ... ");

            var closestDrivers = _userService
                .GetAllDrivers()
                .OrderBy(d => d.CurrentLocation.CalculateDistance(originAddress))
                .Take(2)
                .ToList();

            return closestDrivers;
        }
    }
}
