using Microsoft.AspNetCore.Mvc;
using Service.Services;
using System;

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
        public JsonResult DepositMoney(int userId, [FromBody] Credit credit)
        {
            try
            {
                var user = _creditManager.Deposit(userId, credit.Amount);
                return new JsonResult(user);
            }
            catch (Exception exception)
            {
                return new JsonResult(new { error = exception.Message });
            }
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
