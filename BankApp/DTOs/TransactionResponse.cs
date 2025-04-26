using BankApp.Models;

namespace BankApp.DTOs;

public record TransactionResponse(
    long FromAccountId,
    long? ToAccountId,
    decimal Amount,
    Currency Currency,
    DateTime TransactionDate
);