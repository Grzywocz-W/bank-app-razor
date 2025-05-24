using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BankApp.Models.Enums;

namespace BankApp.Models;

[Table("ACCOUNTS")]
public class Account
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ACCOUNT_ID")]
    public long AccountId { get; init; }

    [Column("BALANCE", TypeName = "decimal(15,2)")]
    public decimal Balance { get; set; }

    [Required] [Column("CURRENCY")] public Currency Currency { get; init; }

    [Column("CLIENT_ID")] public long ClientId { get; init; }

    [ForeignKey("ClientId")] public Client Client { get; init; }
}