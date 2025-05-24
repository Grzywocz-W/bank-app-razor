using AutoMapper;
using BankApp.Constants;
using BankApp.DTOs;
using BankApp.Models;
using BankApp.Repositories;
using BankApp.Validators;

namespace BankApp.Services;

public class AccountService
{
    private readonly AccountRepository _accountRepository;
    private readonly TransactionService _transactionService;
    private readonly CurrencyService _currencyService;
    private readonly AccountValidator _accountValidator;
    private readonly IMapper _mapper;

    public AccountService(
        AccountRepository accountRepository,
        TransactionService transactionService,
        CurrencyService currencyService,
        AccountValidator accountValidator,
        IMapper mapper
    )
    {
        _accountRepository = accountRepository;
        _transactionService = transactionService;
        _currencyService = currencyService;
        _accountValidator = accountValidator;
        _mapper = mapper;
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

        await _accountValidator.ValidateAccountOwnership(fromId);

        var fromAccount = await _accountRepository.FindByIdAsync(fromId);
        var toAccount = await _accountRepository.FindByIdAsync(toId);
        _accountValidator.ValidateAccount(fromAccount);
        _accountValidator.ValidateAccount(toAccount);
        _accountValidator.ValidateAmountAndBalance(amount, fromAccount);

        var convertedAmount = amount;

        if (fromAccount.Currency != toAccount.Currency)
        {
            var rates = await _currencyService.GetCurrencyRatesAsync();

            if (
                !rates!.Rates.TryGetValue(toAccount.Currency.ToString(), out var targetRate) ||
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
        await _accountValidator.ValidateAccountOwnership(id);

        var account = await _accountRepository.FindByIdAsync(id);
        _accountValidator.ValidateAccount(account);
        _accountValidator.ValidateAmountAndBalance(amount, account);

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
        await _accountValidator.ValidateAccountOwnership(accountId);

        var account = await _accountRepository.FindByIdAsync(accountId);
        _accountValidator.ValidateAccount(account);

        if (account.Balance != 0)
            throw new InvalidOperationException("Cannot delete account with non-zero balance.");

        await _accountRepository.DeleteAsync(account);
    }
}