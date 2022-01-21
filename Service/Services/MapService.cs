using Domain.Entities;
using System;

namespace Service.Services
{
    public class MapService
    {
        public Address Search(string text)
        {
            var lat = new Random().Next(1000);
            var lng = new Random().Next(1000);

            return new Address()
            {
                Latitude = lat,
                Longitude = lng,
                Description = text
            };
        }
    }
}
