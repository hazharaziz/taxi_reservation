using Domain.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PassengerRepository : IRepository<Passenger>
    {
        private List<Passenger> _passengers;

        public PassengerRepository()
        {
            _passengers = new List<Passenger>();
        }

        public Passenger GetById(long passengerId)
        {
            return _passengers.FirstOrDefault(p => p.Id == passengerId);
        }

        public void Add(Passenger passenger)
        {
            _passengers.Add(passenger)
        }

        public void Update(Passenger passenger)
        {
            var dbPassenger = _passengers.FirstOrDefault(p => p.Id == passenger.Id);

            if (dbPassenger != null)
                throw new Exception("Passenger does not exist");

            dbPassenger = passenger;
        }

        public void Delete(long passengerId)
        {
            _passengers = _passengers.Where(p => p.Id == passengerId).ToList();
        }
    }
}
