using Domain.Enums;
using System;

namespace Domain.Entities
{
    public class TripOptions
    {
        public long Id { get; set; }

        public Address SecondDestination { get; set; }

        public bool RoundTrip { get; set; }

        public DateTime ExtraStopDuration { get; set; }

        public PaymentType PaymentType { get; set; }

        public bool Hurried { get; set; }
    }
}
