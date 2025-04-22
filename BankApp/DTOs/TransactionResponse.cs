using BankApp.Models;

namespace BankApp.DTOs;

public class TransactionResponse
{
    public long FromAccountId { get; init; }
    public long? ToAccountId { get; init; }
    public decimal Amount { get; init; }
    public Currency Currency { get; init; }
    public DateTime TransactionDate { get; init; }
}