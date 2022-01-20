using Domain.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class TripRepository : IRepository<Trip>
    {
        private List<Trip> _trips;

        public TripRepository()
        {
            _trips = new List<Trip>();
        }

        public Trip GetById(long tripId)
        {
            return _trips.FirstOrDefault(t => t.Id == tripId);
        }

        public void Add(Trip trip)
        {
            _trips.Add(trip);
        }

        public void Update(Trip trip)
        {
            var dbTrip = _trips.FirstOrDefault(t => t.Id == trip.Id);

            if (dbTrip == null)
                throw new Exception("This trip no longer exists");

            dbTrip.Status = trip.Status;
        }

        public void Delete(long tripId)
        {
            _trips = _trips.Where(t => t.Id == tripId).ToList();
        }
    }
}
