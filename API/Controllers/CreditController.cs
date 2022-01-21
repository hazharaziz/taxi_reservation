using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using Service.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/credit")]
    [ApiController]
    public class CreditController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;
        private readonly CreditManager _creditManager;

        public CreditController(IRepository<User> userRepository, CreditManager creditManager)
        {
            _userRepository = userRepository;
            _creditManager = creditManager;
        }

        [HttpPut("deposit/{userId}")]
        public User DepositMoney(int userId, [FromBody] Credit credit)
        {
            var deposited = _creditManager.Deposit(userId, credit.Amount);
            return _userRepository.GetById(userId);
        }

        [HttpPut("withdraw/{userId}")]
        public JsonResult WithdrawMoney(int userId, [FromBody] Credit credit)
        {
            try
            {
                var withdrawn = _creditManager.Withdraw(userId, credit.Amount, credit.CardNumber);
                return new JsonResult(_userRepository.GetById(userId));
            }
            catch (Exception exception)
            {
                return new JsonResult(new { error = exception.Message });
            }
        }
    }

    public class Credit
    {
        public double Amount { get; set; }
        public string CardNumber { get; set; }
    }
}
