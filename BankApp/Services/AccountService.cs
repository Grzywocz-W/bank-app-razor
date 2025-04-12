using BankApp.DTOs;
using BankApp.Models;
using BankApp.Repositories;

namespace BankApp.Services;

public class AccountService
{
    private readonly AccountRepository _accountRepository;
    private readonly ClientRepository _clientRepository;

    public AccountService(AccountRepository accountRepository, ClientRepository clientRepository)
    {
        _accountRepository = accountRepository;
        _clientRepository = clientRepository;
    }

    public async Task SaveAsync(AccountRequest accountRequest)
    {
        var client = await _clientRepository.FindByIdAsync(accountRequest.ClientId);
        if (client == null)
        {
            throw new ArgumentException("Client not found.");
        }

        var account = new Account
        {
            Balance = accountRequest.Balance,
            Currency = accountRequest.Currency,
            ClientId = accountRequest.ClientId
        };

        await _accountRepository.SaveAsync(account);
    }


    public async Task<AccountResponse> FindByIdAsync(long id)
    {
        var account = await _accountRepository.FindByIdAsync(id);
        if (account == null)
        {
            throw new ArgumentException("Account not found");
        }

        return new AccountResponse
        {
            AccountId = account.AccountId,
            Balance = account.Balance,
            Currency = account.Currency,
            ClientId = account.ClientId
        };
    }

    public async Task TransferAsync(
        long fromId,
        long toId,
        decimal amount
    )
    {
        if (fromId == toId)
        {
            throw new ArgumentException("From and To accounts must be different.");
        }

        var fromAccount = await _accountRepository.FindByIdAsync(fromId);
        var toAccount = await _accountRepository.FindByIdAsync(toId);

        if (fromAccount == null || toAccount == null)
        {
            throw new ArgumentException("One or both accounts not found.");
        }

        if (fromAccount.Balance < amount)
        {
            throw new InvalidOperationException("Insufficient funds.");
        }

        fromAccount.Balance -= amount;
        toAccount.Balance += amount;

        await _accountRepository.SaveAsync(fromAccount);
        await _accountRepository.SaveAsync(toAccount);
    }

    public async Task WithdrawAsync(
        long id,
        decimal amount
    )
    {
        var account = await _accountRepository.FindByIdAsync(id);
        if (account == null)
        {
            throw new ArgumentException("Account not found.");
        }

        if (account.Balance < amount)
        {
            throw new InvalidOperationException("Insufficient balance.");
        }

        account.Balance -= amount;
        await _accountRepository.SaveAsync(account);
    }

    public async Task DeleteAsync(long accountId)
    {
        var account = await _accountRepository.FindByIdAsync(accountId);
        if (account == null)
            throw new ArgumentException("Account not found.");

        if (account.Balance != 0)
            throw new InvalidOperationException("Cannot delete account with non-zero balance.");

        await _accountRepository.DeleteAsync(account);
    }
}