using Domain.Entities;
using Repository.Interfaces;
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
            var user = _userRepository.GetById(userId);
            user.Balance += amount;

            // fake bank api delay
            Thread.Sleep(500);
            return true;
        }

        public bool Withdraw(long userId, double amount, string cardNumber)
        {
            var user = _userRepository.GetById(userId);
            user.Balance -= amount;
            
            // fake bank api delay
            Thread.Sleep(500);
            return true;
        }
    }
}
