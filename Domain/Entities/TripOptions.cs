using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TripOptions
    {
        public Address SecondDestination { get; set; }

        public bool RoundTrip { get; set; }

        public DateTime ExtraStopDuration { get; set; }

        public PaymentType PaymentType { get; set; }

        public bool Hurried { get; set; }
    }
}
