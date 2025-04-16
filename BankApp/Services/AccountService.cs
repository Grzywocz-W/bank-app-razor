using BankApp.DTOs;
using BankApp.Models;
using BankApp.Repositories;

namespace BankApp.Services;

public class AccountService
{
    private readonly AccountRepository _accountRepository;

    public AccountService(AccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task Save(AccountRequest accountRequest)
    {
        var account = new Account
        {
            Balance = accountRequest.Balance,
            Currency = accountRequest.Currency,
            ClientId = accountRequest.ClientId
        };

        await _accountRepository.SaveAsync(account);
    }

    public async Task<AccountResponse> FindById(long id)
    {
        var account = await _accountRepository.FindByIdAsync(id);
        if (account == null)
            throw new ArgumentException("Account not found");

        return new AccountResponse
        {
            AccountId = account.AccountId,
            Balance = account.Balance,
            Currency = account.Currency,
            ClientId = account.ClientId
        };
    }

    public async Task Transfer(
        long fromId,
        long toId,
        decimal amount
    )
    {
        if (fromId == toId)
            throw new ArgumentException("From and To accounts must be different.");

        var fromAccount = await _accountRepository.FindByIdAsync(fromId);
        var toAccount = await _accountRepository.FindByIdAsync(toId);

        if (fromAccount == null || toAccount == null)
            throw new ArgumentException("One or both accounts not found.");


        if (fromAccount.Balance < amount)
            throw new InvalidOperationException("Insufficient funds.");

        fromAccount.Balance -= amount;
        toAccount.Balance += amount;

        await _accountRepository.SaveAsync(fromAccount);
        await _accountRepository.SaveAsync(toAccount);
    }

    public async Task Withdraw(
        long id,
        decimal amount
    )
    {
        var account = await _accountRepository.FindByIdAsync(id);
        if (account == null)
            throw new ArgumentException("Account not found.");

        if (account.Balance < amount)
            throw new InvalidOperationException("Insufficient balance.");

        account.Balance -= amount;
        await _accountRepository.SaveAsync(account);
    }

    public async Task Delete(long accountId)
    {
        var account = await _accountRepository.FindByIdAsync(accountId);
        if (account == null)
            throw new ArgumentException("Account not found.");

        if (account.Balance != 0)
            throw new InvalidOperationException("Cannot delete account with non-zero balance.");

        await _accountRepository.DeleteAsync(account);
    }
}