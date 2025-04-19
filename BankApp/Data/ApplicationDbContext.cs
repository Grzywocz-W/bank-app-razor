using BankApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Client>()
            .HasKey(c => c.ClientId);

        modelBuilder.Entity<Account>()
            .HasOne(a => a.Client)
            .WithMany(c => c.Accounts)
            .HasForeignKey(a => a.ClientId);

        modelBuilder.Entity<Client>()
            .HasIndex(c => c.Login)
            .HasDatabaseName("IX_Client_Login")
            .IsUnique();

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.ToTable("TRANSACTIONS");

            entity.HasKey(t => t.Id);

            entity.Property(t => t.Id)
                .HasColumnName("TRANSACTION_ID")
                .ValueGeneratedOnAdd();

            entity.Property(t => t.Amount)
                .HasColumnName("AMOUNT");

            entity.Property(t => t.Currency)
                .HasColumnName("CURRENCY");

            entity.Property(t => t.TransactionDate)
                .HasColumnName("TRANSACTION_DATE");

            entity.Property(t => t.FromAccountId)
                .HasColumnName("FROM_ACCOUNT_ID");

            entity.Property(t => t.ToAccountId)
                .HasColumnName("TO_ACCOUNT_ID");

            entity.HasOne<Account>()
                .WithMany(a => a.OutgoingTransactions)
                .HasForeignKey(t => t.FromAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne<Account>()
                .WithMany(a => a.IncomingTransactions)
                .HasForeignKey(t => t.ToAccountId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}