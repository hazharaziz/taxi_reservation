using Domain.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class DriverRepository : IRepository<Driver>
    {
        private List<Driver> _drivers;

        public DriverRepository()
        {
            _drivers = new List<Driver>();
        }

        public Driver GetById(long driverId)
        {
            return _drivers.FirstOrDefault(d => d.Id == driverId);
        }

        public void Add(Driver driver)
        {
            _drivers.Add(driver);
        }

        public void Update(Driver driver)
        {
            var dbDriver = _drivers.FirstOrDefault(d => d.Id == driver.Id);

            if (dbDriver != null)
                throw new Exception("Driver does not exist");

            dbDriver = driver;
        }

        public void Delete(long driverId)
        {
            _drivers = _drivers.Where(d => d.Id == driverId).ToList();
        } 
    }
}
