using System.ComponentModel.DataAnnotations;
using BankApp.Models;

namespace BankApp.DTOs;

public class AccountRequest
{
    [Required]
    [Range(0.00, 1000000000000)]
    public decimal Balance { get; set; }

    [Required]
    [EnumDataType(typeof(Currency))]
    public Currency Currency { get; set; }

    public long ClientId { get; set; }
}