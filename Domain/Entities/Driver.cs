using Domain.Enums;
using System;

namespace Domain.Entities
{
    public class Driver : User
    {
        public CarType Car { get; set; }

        public string VehicleId { get; set; }

        public Address CurrentLocation { get; set; }

        public bool RespondTripRequest(Trip trip)
        {
            var random = new Random().Next((int)(this.CurrentLocation.Latitude + this.CurrentLocation.Longitude));
            return random % 2 == 0;
        }
    }
}
