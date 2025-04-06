using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankApp.Models
{
    [Table("ACCOUNTS")]
    public class Account
    {
        [Key]
        [Column("ACCOUNT_ID")]
        public long AccountId { get; set; }

        [Column("BALANCE")]
        public double Balance { get; set; }

        [Column("CURRENCY")]
        public string Currency { get; set; }

        [Column("USER_ID")]
        public long? UserId { get; set; }
        
        public string AccountNumber { get; set; }  
    }
}