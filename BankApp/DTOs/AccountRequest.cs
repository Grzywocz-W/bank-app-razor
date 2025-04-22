using System.ComponentModel.DataAnnotations;
using BankApp.Models;

namespace BankApp.DTOs;

public class AccountRequest
{
    public const double MaxBalance = 1_000_000_000_000;

    [Required] [Range(0.00, MaxBalance)] public decimal Balance { get; init; }

    [Required]
    [EnumDataType(typeof(Currency))]
    public Currency Currency { get; init; }

    [Required] public long ClientId { get; set; }
}