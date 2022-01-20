using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ITripRepository : IRepository<Trip>
    {
        public List<Trip> GetUserTrips(User user);

        public Trip GetUserCurrentTrip(User user);
    }
}
