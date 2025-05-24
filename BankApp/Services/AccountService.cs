using AutoMapper;
using BankApp.Constants;
using BankApp.DTOs;
using BankApp.Helpers;
using BankApp.Models;
using BankApp.Repositories;

namespace BankApp.Services;

public class AccountService
{
    private readonly AccountRepository _accountRepository;
    private readonly TransactionService _transactionService;
    private readonly CurrencyService _currencyService;
    private readonly UserHelper _userHelper;
    private readonly IMapper _mapper;

    public AccountService(
        AccountRepository accountRepository,
        TransactionService transactionService,
        CurrencyService currencyService,
        IMapper mapper,
        UserHelper userHelper
    )
    {
        _accountRepository = accountRepository;
        _transactionService = transactionService;
        _currencyService = currencyService;
        _mapper = mapper;
        _userHelper = userHelper;
    }

    public async Task Save(AccountRequest accountRequest)
    {
        if (accountRequest.Balance is < 0 or > (decimal)AccountConstraints.MaxValue)
            throw new ArgumentException("Invalid balance.");

        var account = _mapper.Map<Account>(accountRequest);

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

        if (!await IsAccountOwnedByClient(fromId, _userHelper.GetClientId()))
            throw new UnauthorizedAccessException("You are not the owner of the source account.");

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
                Currency = fromAccount.Currency
            }
        );
    }

    public async Task Withdraw(
        long id,
        decimal amount
    )
    {
        if (!await IsAccountOwnedByClient(id, _userHelper.GetClientId()))
            throw new UnauthorizedAccessException("You are not the owner of the source account.");
        
        var account = await _accountRepository.FindByIdAsync(id);
        ValidateAccount(account);
        ValidateAmountAndBalance(amount, account);

        account.Balance -= amount;
        await _accountRepository.SaveAsync(account);

        await _transactionService.Save(new TransactionRequest
            {
                FromAccountId = id,
                Amount = amount,
                Currency = account.Currency
            }
        );
    }

    public async Task Delete(long accountId)
    {
        if (!await IsAccountOwnedByClient(accountId, _userHelper.GetClientId()))
            throw new UnauthorizedAccessException("You are not the owner of the source account.");
        
        var account = await _accountRepository.FindByIdAsync(accountId);
        ValidateAccount(account);

        if (account.Balance != 0)
            throw new InvalidOperationException("Cannot delete account with non-zero balance.");

        await _accountRepository.DeleteAsync(account);
    }

    private async Task<bool> IsAccountOwnedByClient(long accountId, long clientId)
    {
        return await _accountRepository.IsAccountOwnedByClientAsync(accountId, clientId);
    }

    private static void ValidateAccount(Account? account)
    {
        if (account == null)
            throw new ArgumentException("Account not found.");
    }

    private static void ValidateAmountAndBalance(
        decimal amount,
        Account account
    )
    {
        if (amount is < 0 or > (decimal)AccountConstraints.MaxValue)
            throw new InvalidOperationException("Invalid amount.");

        if (account.Balance < amount)
            throw new InvalidOperationException("Insufficient balance.");
    }
}