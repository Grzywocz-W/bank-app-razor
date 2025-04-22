using BankApp.Models;

namespace BankApp.DTOs;

public class AccountResponse
{
    public long AccountId { get; init; }
    public decimal Balance { get; init; }
    public Currency Currency { get; init; }
    public long ClientId { get; set; }
}