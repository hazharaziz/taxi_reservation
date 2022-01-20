using Domain.Entities;
using System.Collections.Generic;

namespace Repository.Interfaces
{
    public interface ITripRepository : IRepository<Trip>
    {
        public List<Trip> GetUserTrips(User user);

        public Trip GetUserCurrentTrip(User user);
    }
}
