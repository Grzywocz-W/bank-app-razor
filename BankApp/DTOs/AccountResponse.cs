using BankApp.Models;

namespace BankApp.DTOs;

public record AccountResponse(
    long AccountId,
    decimal Balance,
    Currency Currency,
    long ClientId
);