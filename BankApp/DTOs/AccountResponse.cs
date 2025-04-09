namespace BankApp.DTOs;

public class AccountResponse
{
    public long Id { get; set; }
    public decimal Balance { get; set; }
    public string Currency { get; set; }
    public long UserId { get; set; }

    // public AccountResponse(long id, decimal balance, string currency, long userId)
    // {
    //     Id = id;
    //     Balance = balance;
    //     Currency = currency;
    //     UserId = userId;
    // }
}