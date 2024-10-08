namespace Domain.Models
{
    public class Account : BaseModel
    {
        public decimal Balance { get; set; }
        public long UserId { get; set; } 
        public User User { get; set; }  
        public ICollection<Transaction> Transactions { get; set; } 
    }
}
