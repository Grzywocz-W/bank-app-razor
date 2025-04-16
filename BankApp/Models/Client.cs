using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankApp.Models;

[Table("CLIENTS")]
public class Client
{
    [Key] [Column("CLIENT_ID")] public long ClientId { get; set; }

    [Required] [Column("LOGIN")] public string Login { get; set; }

    [Required] [Column("PASSWORD")] public string Password { get; set; }

    [InverseProperty("Client")] public List<Account> Accounts { get; set; } = new();

    public decimal GetBalance() => Accounts.Sum(a => a.Balance);
}