using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using Service.Services;
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
        private readonly CreditService _creditManager;

        public CreditController(CreditService creditManager)
        {
            _creditManager = creditManager;
        }

        [HttpPut("deposit/{userId}")]
        public User DepositMoney(int userId, [FromBody] Credit credit)
        {
            var user = _creditManager.Deposit(userId, credit.Amount);
            return user;
        }

        [HttpPut("withdraw/{userId}")]
        public JsonResult WithdrawMoney(int userId, [FromBody] Credit credit)
        {
            try
            {
                var user = _creditManager.Withdraw(userId, credit.Amount, credit.CardNumber);
                return new JsonResult(user);
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
