using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BankApp.Models.Enums;

namespace BankApp.Models;

[Table("TRANSACTIONS")]
public class Transaction
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("TRANSACTION_ID")]
    public long Id { get; init; }

    [Column("AMOUNT", TypeName = "decimal(15,2)")]
    public decimal Amount { get; init; }

    [Column("CURRENCY")] public Currency Currency { get; init; }

    [Column("TRANSACTION_DATE")] public DateTime TransactionDate { get; init; }

    [Column("FROM_ACCOUNT_ID")] public long FromAccountId { get; init; }

    [Column("TO_ACCOUNT_ID")] public long? ToAccountId { get; init; }

    // (lazy loading)
    public Account? FromAccount { get; init; }
    public Account? ToAccount { get; init; }
}