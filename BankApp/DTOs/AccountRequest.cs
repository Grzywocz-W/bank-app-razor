namespace BankApp.DTOs;

public class AccountRequest
{
    public decimal Balance { get; set; }
    public string Currency { get; set; }
    public long ClientId { get; set; }  
}