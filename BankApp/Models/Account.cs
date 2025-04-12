using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankApp.Models;

[Table("ACCOUNTS")]
public class Account
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ACCOUNT_ID")]
    public long AccountId { get; set; }

    [Column("BALANCE")] public decimal Balance { get; set; }

    [Required] [Column("CURRENCY")] public string Currency { get; set; }

    [Column("CLIENT_ID")] public long ClientId { get; set; }

    [ForeignKey("ClientId")] public Client Client { get; set; }
}