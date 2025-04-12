namespace BankApp.DTOs;

public class AccountResponse
{
    public long AccountId { get; set; }
    public decimal Balance { get; set; }
    public string Currency { get; set; }
    public long ClientId { get; set; }
}