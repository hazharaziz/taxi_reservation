using Domain.Entities;
using Domain.Enums;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Repositories
{
    public class TripRepository : ITripRepository
    {
        private List<Trip> _trips;

        public TripRepository()
        {
            _trips = new List<Trip>();
        }

        public List<Trip> GetAll()
        {
            return _trips;
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

        public List<Trip> GetUserTrips(User user)
        {
            return _trips
                .Where(t => t.Passenger.Id == user.Id || t.Driver.Id == user.Id)
                .ToList();
        }

        public Trip GetUserCurrentTrip(User user)
        {
            return _trips
                .FirstOrDefault(t => (t.Passenger.Id == user.Id || t.Driver.Id == user.Id)
                                    && t.Status == TripStatus.OnTheWay);
        }
    }
}
