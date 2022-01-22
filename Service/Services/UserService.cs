using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Services
{
    public class UserService
    {
        private List<User> users;

        public UserService()
        {
            users = new List<User>()
            {
                new Driver()
                {
                    Id = 1,
                    Name = "Jack Wilson",
                    Phone = "+12063710615",
                    Email = "jack_wilson@gmail.com",
                    Gender = GenderType.Male,
                    Balance = 0,
                    BirthDate = new DateTime(2020, 12, 1),
                    Car = CarType.BlueNisan,
                    CurrentLocation = new Address()
                    {
                        Latitude = 40.635276407220324,
                        Longitude = -73.9522972821614,
                        Description = "Brooklyn College"
                    }
                },
                new Passenger()
                {
                    Id = 2,
                    Name = "John Doe",
                    Phone = "+12064710612",
                    Email = "john_doe@gmail.com",
                    Gender = GenderType.Male,
                    Balance = 0,
                    BirthDate = new DateTime(2020, 11, 2),
                    DefaultAddress = new Address()
                    {
                        Latitude = 40.6789889020544,
                        Longitude = -73.83362408096723,
                        Description = "Resorts World Casino New York City"
                    }
                },
                new Driver()
                {
                    Id = 3,
                    Name = "Marry Jane",
                    Phone = "+12065710611",
                    Email = "marry_jane@gmail.com",
                    Gender = GenderType.Female,
                    Balance = 0,
                    BirthDate = new DateTime(2020, 10, 2),
                    Car = CarType.YouthsPeikan,
                    CurrentLocation = new Address()
                    {
                        Latitude = 40.60608983008915,
                        Longitude = -73.93547447033153,
                        Description = "Marine Park"
                    }
                },
                new Passenger()
                {
                    Id = 4,
                    Name = "Christina Lopez",
                    Phone = "+12065810613",
                    Email = "christina_lopez@gmail.com",
                    Gender = GenderType.Female,
                    Balance = 0,
                    BirthDate = new DateTime(2020, 9, 1),
                    DefaultAddress = new Address()
                    {
                        Latitude = 40.610263635204774,
                        Longitude = -74.04443891820534,
                        Description = "Verrazzano-Narrows Bridge"
                    }
                },
                new Driver()
                {
                    Id = 5,
                    Name = "John Joe Shelvy",
                    Phone = "+12065720667",
                    Email = "john_shelvy@gmail.com",
                    Gender = GenderType.Male,
                    Balance = 0,
                    BirthDate = new DateTime(2020, 10, 12),
                    Car = CarType.Pride,
                    CurrentLocation = new Address()
                    {
                        Latitude = 40.66132507296242,
                        Longitude = -73.91178520219533,
                        Description = "Brookdale University Hospital Medical Center"
                    }
                },
                new Passenger()
                {
                    Id = 6,
                    Name = "Maria Gomez",
                    Phone = "+12065810624",
                    Email = "maria_gomez@gmail.com",
                    Gender = GenderType.Female,
                    Balance = 0,
                    BirthDate = new DateTime(2020, 9, 10),
                    DefaultAddress = new Address()
                    {
                        Latitude = 40.623195624934425,
                        Longitude = -74.01384672724215,
                        Description = "Dyker Heights Christmas Lights"
                    }
                },
                new Driver()
                {
                    Id = 7,
                    Name = "Zlatan Ibrahimovic",
                    Phone = "+12065890667",
                    Email = "zlatan_ibra@gmail.com",
                    Gender = GenderType.Male,
                    Balance = 0,
                    BirthDate = new DateTime(2020, 5, 12),
                    Car = CarType.Renault,
                    CurrentLocation = new Address()
                    {
                        Latitude = 40.756117864817426,
                        Longitude = -73.98072871608446,
                        Description = "Empire State Building"
                    }
                },
                new Passenger()
                {
                    Id = 8,
                    Name = "Lionel Messi",
                    Phone = "+12065810623",
                    Email = "lionel_messi@gmail.com",
                    Gender = GenderType.Male,
                    Balance = 0,
                    BirthDate = new DateTime(2020, 9, 15),
                    DefaultAddress = new Address()
                    {
                        Latitude = 40.75402348926799,
                        Longitude = -73.97260627967285,
                        Description = "Chrysler Building"
                    }
                },
                new Driver()
                {
                    Id = 9,
                    Name = "Cristiano Ronaldo",
                    Phone = "+12065890888",
                    Email = "chris_ronaldo@gmail.com",
                    Gender = GenderType.Male,
                    Balance = 0,
                    BirthDate = new DateTime(2020, 5, 29),
                    Car = CarType.Peugeot,
                    CurrentLocation = new Address()
                    {
                        Latitude = 40.78393903507809,
                        Longitude = 73.96630916282228,
                        Description = "Central Park"
                    }
                },
                new Passenger()
                {
                    Id = 8,
                    Name = "Daniel Craig",
                    Phone = "+12065810611",
                    Email = "daniel_craig@gmail.com",
                    Gender = GenderType.Male,
                    Balance = 0,
                    BirthDate = new DateTime(2020, 12, 15),
                    DefaultAddress = new Address()
                    {
                        Latitude = 40.79647363044284,
                        Longitude = -73.95207994129665,
                        Description = "Museum of the City of New York"
                    }
                },
                new Driver()
                {
                    Id = 11,
                    Name = "Tom Cruise",
                    Phone = "+12065896688",
                    Email = "tom_cruise@gmail.com",
                    Gender = GenderType.Male,
                    Balance = 0,
                    BirthDate = new DateTime(2020, 5, 27),
                    Car = CarType.YouthsPeikan,
                    CurrentLocation = new Address()
                    {
                        Latitude = 40.737588710570456,
                        Longitude = -74.0052917432043,
                        Description = "Friends Apartment"
                    }
                }

            };
        }

        public List<User> GetAllUsers()
        {
            return users;
        }

        public List<Driver> GetAllDrivers()
        {
            return users
                .Where(u => u is Driver)
                .Select(u => u as Driver)
                .ToList();
        }

        public List<Passenger> GetAllPassengers()
        {
            return users
                .Where(u => u is Passenger)
                .Select(u => u as Passenger)
                .ToList();
        }

        public User GetUserById(long userId)
        {
            return users.FirstOrDefault(u => u.Id == userId);
        }

        public Driver GetDriverById(long driverId)
        {
            var user = GetUserById(driverId);

            if (user is Driver)
                return user as Driver;

            return null;
        }

        public Passenger GetPassengerById(long passengerId)
        {
            var user = GetUserById(passengerId);

            if (user is Passenger)
                return user as Passenger;

            return null;
        }
    }
}
