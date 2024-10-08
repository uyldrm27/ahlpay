namespace Domain.DTOs
{
    public class AddTransactionDto
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public long CategoryId { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
