namespace Domain.DTOs
{
    public class CreateAccountDto
    {
        public long UserId { get; set; }
        public decimal InitialBalance { get; set; }
    }
}
