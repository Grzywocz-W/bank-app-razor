using BankApp.Models;

namespace BankApp.DTOs;

public class TransactionResponse
{
    public long FromAccountId { get; set; }
    public long? ToAccountId { get; set; }
    public decimal Amount { get; set; }
    public Currency Currency { get; set; }
    public DateTime TransactionDate { get; set; }
}