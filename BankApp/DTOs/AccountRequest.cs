namespace BankApp.DTOs
{
    public class AccountRequest
    {
        public double Balance { get; set; }
        public string Currency { get; set; }
        public long UserId { get; set; }
    }
}