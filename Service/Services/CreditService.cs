using Domain.Entities;
using System;
using System.Threading;

namespace Service.Services
{
    public class CreditService
    {
        private readonly UserService _userService;

        public CreditService(UserService userService)
        {
            _userService = userService;
        }

        public User Deposit(long userId, double amount)
        {
            Console.WriteLine("Waiting for bank API to deposit the money");
            Thread.Sleep(500);

            var user = _userService.GetUserById(userId);

            if (user == null)
                throw new Exception("User does not exist");

            user.Balance += amount;

            Console.WriteLine($"{amount} deposited in {user.Name}'s balance");

            return user;
        }

        public User Withdraw(long userId, double amount, string cardNumber)
        {
            if (cardNumber == null)
                throw new Exception("Card number not provided!");

            Console.WriteLine("Waiting for bank API to withdraw the money");
            Thread.Sleep(500);

            var user = _userService.GetUserById(userId);

            if (user == null)
                throw new Exception("User does not exist");

            user.Balance -= amount;

            Console.WriteLine($"{amount} Withdrawn from {user.Name}'s balance");

            return user;
        }
    }
}
