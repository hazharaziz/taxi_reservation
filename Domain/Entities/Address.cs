using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Address
    {
        public long Id { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Description { get; set; }

        public double CalculateDistance(Address destination)
        {
            // this is a joke

            return Math.Sqrt(
                Math.Pow(this.Latitude - destination.Latitude, 2) +
                Math.Pow(this.Longitude - destination.Longitude, 2)); 
        }
    }
}
