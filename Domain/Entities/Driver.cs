using Domain.Enums;
using System;

namespace Domain.Entities
{
    public class Driver : User
    {
        public CarType Car { get; set; }

        public string VehicleId { get; set; }

        public Address CurrentLocation { get; set; }
    }
}
