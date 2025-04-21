using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankApp.Models;

[Table("TRANSACTIONS")]
public class Transaction
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("TRANSACTION_ID")]
    public long Id { get; set; }

    [Column("AMOUNT")] public decimal Amount { get; set; }

    [Column("CURRENCY")] public Currency Currency { get; set; }

    [Column("TRANSACTION_DATE")] public DateTime TransactionDate { get; set; }

    [Column("FROM_ACCOUNT_ID")] public long FromAccountId { get; set; }

    [Column("TO_ACCOUNT_ID")] public long? ToAccountId { get; set; }
    
    // (lazy loading, optional)
    public Account? FromAccount { get; set; }
    public Account? ToAccount { get; set; }
}