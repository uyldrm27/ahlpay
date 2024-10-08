namespace Domain.Models
{
    public class Category : BaseModel
    {
        public string Name { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
