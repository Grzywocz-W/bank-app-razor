using BankApp.Models.Enums;

namespace BankApp.DTOs;

public record AccountResponse(
    long AccountId,
    decimal Balance,
    Currency Currency
);