using BankApp.Models;
using BankApp.Data;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Repositories;

public class AccountRepository
{
    private readonly ApplicationDbContext _context;

    public AccountRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task SaveAsync(Account account)
    {
        if (account.AccountId == 0)
        {
            await _context.Accounts.AddAsync(account);
        }
        else
        {
            _context.Accounts.Update(account);
        }

        await _context.SaveChangesAsync();
    }

    public async Task<Account> FindByIdAsync(long id)
    {
        return await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == id);
    }

    public async Task DeleteAsync(Account account)
    {
        _context.Accounts.Remove(account);
        await _context.SaveChangesAsync();
    }
}