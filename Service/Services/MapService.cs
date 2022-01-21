using Domain.Entities;
using System;

namespace Service.Services
{
    public class MapService
    {
        public static Address Search(string text)
        {
            var lat = new Random().Next(-100, 100);
            var lng = new Random().Next(-100, 100);

            return new Address()
            {
                Latitude = lat,
                Longitude = lng,
                Description = text
            };
        }
    }
}
