﻿using Domain.Enums;

namespace Domain.Entities
{
    public class Trip
    {
        public long Id { get; set; }

        public Address Origin { get; set; }

        public Address Destination { get; set; }

        public TripOptions Options { get; set; }

        public Passenger Passenger { get; set; }

        public Driver Driver { get; set; }

        public double Price { get; set; }

        public TripStatus Status { get; set; }
    }
}
