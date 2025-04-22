namespace BankApp.DTOs;

public class CurrencyResponse
{
    public string Base { get; set; }
    public string Date { get; set; }
    public Dictionary<string, decimal> Rates { get; set; }
}