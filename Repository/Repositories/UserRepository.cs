using Domain.Entities;
using Domain.Enums;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private List<User> _users;

        public UserRepository()
        {
            _users = new List<User>()
            {
                new User()
                {
                    Id = 1,
                    Name = "Jack Wilson",
                    Phone = "09123456789",
                    Email = "jack_wilson@gmail.com",
                    Gender = GenderType.Male,
                    Balance = 0,
                    BirthDate = new DateTime(2020, 12, 2)
                },
                new User()
                {
                    Id = 2,
                    Name = "John Doe",
                    Phone = "09123456788",
                    Email = "john_doe@gmail.com",
                    Gender = GenderType.Male,
                    Balance = 0,
                    BirthDate = new DateTime(2020, 11, 2)
                },
                new User()
                {
                    Id = 3,
                    Name = "Marry Jane",
                    Phone = "09123456787",
                    Email = "marry_jane@gmail.com",
                    Gender = GenderType.Female,
                    Balance = 0,
                    BirthDate = new DateTime(2020, 10, 2)
                },
                new User()
                {
                    Id = 4,
                    Name = "Christina Lopez",
                    Phone = "09123456786",
                    Email = "christina_lopez@gmail.com",
                    Gender = GenderType.Female,
                    Balance = 0,
                    BirthDate = new DateTime(2020, 9, 2)
                },
            };
        }

        public List<User> GetAll()
        {
            return _users;
        }

        public User GetById(long userId)
        {
            return _users.FirstOrDefault(u => u.Id == userId);
        }

        public void Add(User user)
        {
            _users.Add(user);
        }

        public void Update(User user)
        {
            var dbUser = _users.FirstOrDefault(u => u.Id == user.Id);

            if (dbUser == null)
                throw new Exception("User does not exist");

            dbUser = user;
        }

        public void Delete(long userId)
        {
            _users = _users.Where(u => u.Id != userId).ToList();
        }
    }
}
