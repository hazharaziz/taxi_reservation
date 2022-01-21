using Domain.Entities;
using Repository.Interfaces;
using System;
using System.Threading;

namespace Service.Managers
{
    public class CreditManager
    {
        private readonly IRepository<User> _userRepository;

        public CreditManager(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public bool Deposit(long userId, double amount)
        {
            Console.WriteLine("Waiting for bank API to deposit the money");
            Thread.Sleep(500);

            var user = _userRepository.GetById(userId);
            user.Balance += amount;

            Console.WriteLine($"{amount} deposited in {user.Name}'s balance");

            return true;
        }

        public bool Withdraw(long userId, double amount, string cardNumber)
        {
            if (cardNumber == null)
                throw new Exception("Card number not provided!");

            Console.WriteLine("Waiting for bank API to withdraw the money");
            Thread.Sleep(500);

            var user = _userRepository.GetById(userId);
            user.Balance -= amount;

            Console.WriteLine($"{amount} Withdrawn from {user.Name}'s balance");

            return true;
        }
    }
}
