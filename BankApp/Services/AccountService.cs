using BankApp.Models;
using BankApp.Repositories;
using BankApp.DTOs;

namespace BankApp.Services;

public class AccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public void Save(AccountRequest accountRequest)
    {
        var account = new Account
        {
            Balance = accountRequest.Balance,
            Currency = accountRequest.Currency,
            UserId = accountRequest.UserId
        };
        _accountRepository.Save(account);
    }

    public AccountResponse FindById(long id)
    {
        var account = _accountRepository.FindById(id);
        if (account == null)
        {
            throw new ArgumentException("Account not found");
        }

        return new AccountResponse
        {
            Id = account.AccountId,
            Balance = account.Balance,
            Currency = account.Currency,
            UserId = account.UserId ?? 0,
        };
    }

    public void Transfer(long fromId, long toId, decimal amount)
    {
        if (fromId == toId)
        {
            throw new ArgumentException("fromId and toId can't be equal!");
        }

        var fromAccount = _accountRepository.FindById(fromId);
        var toAccount = _accountRepository.FindById(toId);

        if (fromAccount == null || toAccount == null)
        {
            throw new ArgumentException("Source or target account not found");
        }

        if (fromAccount.Balance >= amount)
        {
            fromAccount.Balance -= amount;
            toAccount.Balance += amount;
        }
        else
        {
            throw new Exception("Not enough funds!");
        }

        _accountRepository.Save(fromAccount);
        _accountRepository.Save(toAccount);
    }

    public void WithDraw(long id, decimal amount)
    {
        var account = _accountRepository.FindById(id);
        if (account == null)
        {
            throw new ArgumentException("Source account not found");
        }

        if (account.Balance < amount)
        {
            throw new Exception("Balance must be >= than amount!");
        }

        account.Balance -= amount;
        _accountRepository.Save(account);
    }
}