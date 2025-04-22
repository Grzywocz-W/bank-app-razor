using BankApp.Data;
using BankApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Repositories;

public class TransactionRepository
{
    private readonly ApplicationDbContext _context;

    public TransactionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task SaveAsync(Transaction transaction)
    {
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Transaction>> GetTransactionsByAccountIdAsync(long accountId)
    {
        return await _context.Transactions
            .Where(t => t.FromAccountId == accountId || t.ToAccountId == accountId)
            .OrderByDescending(t => t.TransactionDate)
            .ToListAsync();
    }
}