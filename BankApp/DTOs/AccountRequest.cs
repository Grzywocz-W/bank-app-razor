namespace BankApp.DTOs;

public class AccountRequest
{
    public decimal Balance { get; set; }
    public string Currency { get; set; }
    public long UserId { get; set; }

    public AccountRequest(decimal balance, string currency)
    {
        Balance = balance;
        Currency = currency;
    }
}