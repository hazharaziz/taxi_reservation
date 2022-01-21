using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class UserService
    {
        private List<User> _users;

        public UserService()
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

        public User GetUserById(long userId)
        {
            return _users.FirstOrDefault(u => u.Id == userId);
        }
    }
}
