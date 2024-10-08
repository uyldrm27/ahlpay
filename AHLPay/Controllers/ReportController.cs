using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AHLPay.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class ReportController : ControllerBase
    {
        private readonly AhlDbContext _context;

        public ReportController(AhlDbContext context)
        {
            _context = context;
        }

        [HttpGet("monthly-summary/{userId}")]
        public IActionResult GetMonthlySummary(long userId, [FromQuery] string month)
        {
            var currentYear = DateTime.Now.Year;

            if (string.IsNullOrEmpty(month) || month.Length != 2 || !int.TryParse(month, out int monthInt) || monthInt < 1 || monthInt > 12)
            {
                return BadRequest("Invalid month format. Please provide a valid month in 'MM' format.");
            }

            var accounts = _context.Accounts.Where(a => a.UserId == userId).ToList();
            if (!accounts.Any())
            {
                return NotFound("No accounts found for this user.");
            }

            var transactions = _context.Transactions
                .Where(t => accounts.Select(a => a.Id).Contains(t.AccountId) && t.TransactionDate.Year == currentYear && t.TransactionDate.Month == monthInt)
                .ToList();

            var incomeTotal = transactions.Where(t => t.Amount > 0).Sum(t => t.Amount);
            var expenseTotal = transactions.Where(t => t.Amount < 0).Sum(t => t.Amount);

            return Ok(new
            {
                IncomeTotal = incomeTotal,
                ExpenseTotal = Math.Abs(expenseTotal),
                NetBalance = incomeTotal + expenseTotal
            });
        }

        [HttpGet("spending-predictions/{userId}")]
        public IActionResult GetSpendingPredictions(long userId)
        {
            var accounts = _context.Accounts.Where(a => a.UserId == userId).ToList();
            if (!accounts.Any())
            {
                return NotFound("No accounts found for this user.");
            }

            var transactions = _context.Transactions
                .Where(t => accounts.Select(a => a.Id).Contains(t.AccountId))
                .Include(t => t.Category)  
                .ToList();

            var predictions = transactions
                .GroupBy(t => t.Category.Name)
                .Select(g => new
                {
                    Category = g.Key,
                    PredictedAmount = g.Average(t => t.Amount)
                })
                .ToList();

            if (!predictions.Any())
            {
                return NotFound("No spending predictions available.");
            }

            return Ok(predictions);
        }
    }

}
