using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankApp.Models;

[Table("CLIENTS")]
public class Client
{
    [Key] [Column("CLIENT_ID")] public long ClientId { get; init; }

    [Required] [Column("LOGIN")] public string Login { get; init; }

    [Required] [Column("PASSWORD")] public string Password { get; init; }

    [InverseProperty("Client")] public List<Account> Accounts { get; init; } = new();

    public decimal GetBalance() => Accounts.Sum(a => a.Balance);
}