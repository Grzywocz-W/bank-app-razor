using System.ComponentModel.DataAnnotations;
using BankApp.Models;

namespace BankApp.DTOs;

public class AccountRequest
{
    public const double MaxBalance = 1_000_000_000_000;

    [Required] [Range(0.00, MaxBalance)] public decimal Balance { get; set; }

    [Required]
    [EnumDataType(typeof(Currency))]
    public Currency Currency { get; set; }

    [Required] public long ClientId { get; set; }
}