using System.ComponentModel.DataAnnotations;
using BankApp.Models;

namespace BankApp.DTOs;

public class TransactionRequest
{
    public const double MaxAmount = 1_000_000_000_000;

    [Required] [Range(0.00, MaxAmount)] public decimal Amount;

    [Required] [EnumDataType(typeof(Currency))]
    public Currency Currency;

    [Required] public long FromAccountId;
    [Required] public long? ToAccountId;
}