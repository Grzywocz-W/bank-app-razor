using BankApp.Models;
using BankApp.Data;

namespace BankApp.Repositories;

public interface IAccountRepository
{
    void Save(Account account);
    Account FindById(long id);
}

public class AccountRepository : IAccountRepository
{
    private readonly ApplicationDbContext _context;

    public AccountRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Save(Account account)
    {
        if (account.AccountId == 0)
        {
            _context.Accounts.Add(account);
        }
        else
        {
            _context.Accounts.Update(account);
        }

        _context.SaveChanges();
    }

    public Account FindById(long id)
    {
        return _context.Accounts.FirstOrDefault(a => a.AccountId == id);
    }
}