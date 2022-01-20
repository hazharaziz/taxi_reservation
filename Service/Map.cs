using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class Map
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
