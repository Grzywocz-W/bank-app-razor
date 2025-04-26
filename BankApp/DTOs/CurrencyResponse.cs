namespace BankApp.DTOs;

public record CurrencyResponse(
    Dictionary<string, decimal> Rates
);