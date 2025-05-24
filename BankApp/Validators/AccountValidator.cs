using BankApp.Constants;
using BankApp.Helpers;
using BankApp.Models;
using BankApp.Repositories;

namespace BankApp.Validators;

public class AccountValidator
{
    private readonly AccountRepository _accountRepository;
    private readonly UserHelper _userHelper;

    public AccountValidator(
        AccountRepository accountRepository,
        UserHelper userHelper
    )
    {
        _accountRepository = accountRepository;
        _userHelper = userHelper;
    }

    public void ValidateAccount(Account? account)
    {
        if (account == null)
            throw new ArgumentException("Account not found.");
    }

    public void ValidateAmountAndBalance(
        decimal amount,
        Account account
    )
    {
        if (amount is < 0 or > (decimal)AccountConstraints.MaxValue)
            throw new InvalidOperationException("Invalid amount.");

        if (account.Balance < amount)
            throw new InvalidOperationException("Insufficient balance.");
    }

    public async Task ValidateAccountOwnership(long accountId)
    {
        var clientId = _userHelper.GetClientId();
        if (!await _accountRepository.IsAccountOwnedByClientAsync(accountId, clientId))
            throw new UnauthorizedAccessException("You are not the owner of the source account.");
    }
}