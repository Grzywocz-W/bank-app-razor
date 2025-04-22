using BankApp.DTOs;
using BankApp.Models;
using BankApp.Repositories;

namespace BankApp.Services;

public class AccountService
{
    private readonly AccountRepository _accountRepository;
    private readonly TransactionService _transactionService;
    private readonly CurrencyService _currencyService;

    public AccountService(
        AccountRepository accountRepository,
        TransactionService transactionService,
        CurrencyService currencyService
    )
    {
        _accountRepository = accountRepository;
        _transactionService = transactionService;
        _currencyService = currencyService;
    }

    public async Task Save(AccountRequest accountRequest)
    {
        if (accountRequest.Balance is < 0 or > (decimal)AccountRequest.MaxBalance)
            throw new ArgumentException("Invalid balance.");

        var account = new Account
        {
            Balance = accountRequest.Balance,
            Currency = accountRequest.Currency,
            ClientId = accountRequest.ClientId
        };

        await _accountRepository.SaveAsync(account);
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
        ValidateAccount(fromAccount);
        ValidateAccount(toAccount);
        ValidateAmountAndBalance(amount, fromAccount);

        var convertedAmount = amount;

        if (fromAccount.Currency != toAccount.Currency)
        {
            var rates = await _currencyService.GetCurrencyRatesAsync();

            if (
                !rates.Rates.TryGetValue(toAccount.Currency.ToString(), out var targetRate) ||
                !rates.Rates.TryGetValue(fromAccount.Currency.ToString(), out var sourceRate)
            )
                throw new InvalidOperationException("Currency rate not available.");

            convertedAmount = amount / sourceRate * targetRate;
            convertedAmount = Math.Round(convertedAmount, 2);
        }

        fromAccount.Balance -= amount;
        toAccount.Balance += convertedAmount;

        await _accountRepository.SaveAsync(fromAccount);
        await _accountRepository.SaveAsync(toAccount);

        await _transactionService.Save(new TransactionRequest
            {
                FromAccountId = fromId,
                ToAccountId = toId,
                Amount = amount,
                Currency = fromAccount.Currency,
            }
        );
    }

    public async Task Withdraw(
        long id,
        decimal amount
    )
    {
        var account = await _accountRepository.FindByIdAsync(id);
        ValidateAccount(account);
        ValidateAmountAndBalance(amount, account);

        account.Balance -= amount;
        await _accountRepository.SaveAsync(account);

        await _transactionService.Save(new TransactionRequest
            {
                FromAccountId = id,
                Amount = amount,
                Currency = account.Currency,
            }
        );
    }

    public async Task Delete(long accountId)
    {
        var account = await _accountRepository.FindByIdAsync(accountId);
        ValidateAccount(account);

        if (account.Balance != 0)
            throw new InvalidOperationException("Cannot delete account with non-zero balance.");

        await _accountRepository.DeleteAsync(account);
    }

    private static void ValidateAccount(Account account)
    {
        if (account == null)
            throw new ArgumentException("Account not found.");
    }

    private static void ValidateAmountAndBalance(decimal amount, Account account)
    {
        if (amount is < 0 or > (decimal)TransactionRequest.MaxAmount)
            throw new InvalidOperationException("Invalid amount.");

        if (account.Balance < amount)
            throw new InvalidOperationException("Insufficient balance.");
    }
}