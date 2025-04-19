using BankApp.Models;

namespace BankApp.DTOs;

public class TransactionRequest
{
    public decimal Amount;
    public Currency Currency;
    public long FromAccountId;
    public long? ToAccountId;
}