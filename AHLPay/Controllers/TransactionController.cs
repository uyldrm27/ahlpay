using Domain.DTOs;
using Domain.Models;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AHLPay.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    public class TransactionController : ControllerBase
    {
        private readonly AhlDbContext _context;

        public TransactionController(AhlDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetTransactions(long accountId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var transactions = _context.Transactions.Include(t => t.Category)
                .Where(t => t.AccountId == accountId && t.TransactionDate >= startDate && t.TransactionDate <= endDate)
                .ToList();

            if (!transactions.Any())
            {
                return NotFound("No transactions found for the given date range.");
            }

            return Ok(transactions.Select(t => new
            {
                TransactionId = t.Id,
                Amount = t.Amount,
                Description = t.Description,
                CategoryId = t.CategoryId,
                CategoryName = t.Category.Name,
                TransactionDate = t.TransactionDate
            }));
        }

        [HttpPost("{accountId}")]
        public IActionResult AddTransaction(long accountId, [FromBody] AddTransactionDto transactionDto)
        {
            var account = _context.Accounts.Find(accountId);
            if (account == null)
            {
                return NotFound("Account not found");
            }

            var category = _context.Categories.Find(transactionDto.CategoryId);
            if (category == null)
            {
                return NotFound("Category not found");
            }

            if (transactionDto.Amount < 0)
            {
                if (account.Balance + transactionDto.Amount < 0)
                {
                    return BadRequest("Insufficient balance for this expense.");
                }
            }

            var transaction = new Transaction
            {
                Amount = transactionDto.Amount,
                Description = transactionDto.Description,
                AccountId = account.Id,
                CategoryId = category.Id,
                TransactionDate = transactionDto.TransactionDate,
                CreatedAt = DateTime.Now
            };

            account.Balance += transactionDto.Amount;
            account.UpdatedAt = DateTime.Now;
            _context.Transactions.Add(transaction);
            _context.SaveChanges();

            return Ok(new
            {
                TransactionId = transaction.Id,
                Amount = transaction.Amount,
                Description = transaction.Description,
                CategoryId = category.Id,
                CategoryName = category.Name,
                TransactionDate = transaction.TransactionDate,
                AccountId = account.Id,
                UpdatedBalance = account.Balance 
            });
        }
    }

}
