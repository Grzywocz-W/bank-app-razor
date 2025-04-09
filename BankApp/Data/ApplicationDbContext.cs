using BankApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Account> Accounts { get; set; }

    // Konstruktor DbContext, który akceptuje DbContextOptions
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        // Usunięcie throw new NotImplementedException();
        // Teraz nie rzucamy wyjątku, tylko pozwalamy na inicjalizację DbContext z przekazanymi opcjami
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Ustawienie klucza głównego dla Client, przy założeniu, że UserId jest kluczem głównym
        modelBuilder.Entity<Client>()
            .HasKey(c => c.UserId);  

        // Relacja między Client a Account
        modelBuilder.Entity<Client>()
            .HasMany(c => c.Accounts)
            .WithOne()
            .HasForeignKey(a => a.UserId);  // Zakładając, że Account ma ClientId jako klucz obcy
    }
}