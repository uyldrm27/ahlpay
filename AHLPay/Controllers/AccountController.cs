using Domain.DTOs;
using Domain.Models;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace AHLPay.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController : ControllerBase
    {
        private readonly AhlDbContext _context;

        public AccountController(AhlDbContext context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public IActionResult CreateAccount([FromBody] CreateAccountDto accountDto)
        {
            var user = _context.Users.Find(accountDto.UserId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var account = new Account
            {
                UserId = user.Id,
                Balance = accountDto.InitialBalance,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _context.Accounts.Add(account);
            _context.SaveChanges();

            return Ok(new { AccountId = account.Id, UserId = account.UserId, Balance = account.Balance });
        }        
    }

}
