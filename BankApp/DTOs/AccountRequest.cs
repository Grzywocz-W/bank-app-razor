using System.ComponentModel.DataAnnotations;

namespace BankApp.DTOs;

public class AccountRequest
{
    [Required] 
    [Range(0.00, double.MaxValue)]
    public decimal Balance { get; set; }

    [Required] public string Currency { get; set; }

    public long ClientId { get; set; }
}