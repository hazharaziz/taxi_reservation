using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Driver : User
    {
        public CarType Car { get; set; }

        public string UserId { get; set; }

        public Address CurrentLocation { get; set; }

        public bool RespondTripRequest(Trip trip)
        {
            var random = new Random().Next((int)(this.CurrentLocation.Latitude + this.CurrentLocation.Longitude));
            return random % 2 == 0;
        }
    }
}
