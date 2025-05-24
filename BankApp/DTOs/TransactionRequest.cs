using System.ComponentModel.DataAnnotations;
using BankApp.Constants;
using BankApp.Models.Enums;

namespace BankApp.DTOs;

public class TransactionRequest
{
    [Required]
    [Range(0.00, AccountConstraints.MaxValue)]
    public decimal Amount { get; init; }

    [Required]
    [EnumDataType(typeof(Currency))]
    public Currency Currency { get; init; }

    [Required] public long FromAccountId { get; init; }
    [Required] public long? ToAccountId { get; init; }
}