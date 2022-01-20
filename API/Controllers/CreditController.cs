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

        [HttpPost("deposit/{id}")]
        public User DepositMoney(int id, [FromBody] double amount)
        {
            var deposited = _creditManager.Deposit(id, amount);
            return _userRepository.GetById(id);
        }
    }
}
