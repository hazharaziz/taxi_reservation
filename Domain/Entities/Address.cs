using System;

namespace Domain.Entities
{
    public class Address
    {
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
