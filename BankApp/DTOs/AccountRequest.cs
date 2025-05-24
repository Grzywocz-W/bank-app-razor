using System.ComponentModel.DataAnnotations;
using BankApp.Constants;
using BankApp.Models.Enums;

namespace BankApp.DTOs;

public class AccountRequest
{
    [Required]
    [Range(0.00, AccountConstraints.MaxValue)]
    public decimal Balance { get; init; }

    [Required]
    [EnumDataType(typeof(Currency))]
    public Currency Currency { get; init; }

    [Required] public long ClientId { get; set; }
}