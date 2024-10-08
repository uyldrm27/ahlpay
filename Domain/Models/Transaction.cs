namespace Domain.Models
{
    public class Transaction
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        public long AccountId { get; set; } 
        public Account Account { get; set; }
        public long CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
