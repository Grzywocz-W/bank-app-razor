using BankApp.DTOs;
using BankApp.Models;
using BankApp.Repositories;

namespace BankApp.Services;

public class TransactionService
{
    private readonly TransactionRepository _transactionRepository;
    private readonly AccountService _accountService;

    public TransactionService(TransactionRepository transactionRepository, AccountService accountService)
    {
        _transactionRepository = transactionRepository;
        _accountService = accountService;
    }

    public async Task Save(TransactionRequest request)
    {
        if (request.ToAccountId == null)
        {
            await _accountService.Withdraw(
                request.FromAccountId,
                request.Amount
            );
        }
        else
        {
            await _accountService.Transfer(
                request.FromAccountId,
                request.ToAccountId.Value,
                request.Amount
            );
        }

        var transaction = new Transaction
        {
            Amount = request.Amount,
            Currency = request.Currency,
            FromAccountId = request.FromAccountId,
            ToAccountId = request.ToAccountId,
            TransactionDate = DateTime.UtcNow
        };

        await _transactionRepository.SaveAsync(transaction);
    }

    public async Task<List<TransactionResponse>> GetByAccountId(long accountId)
    {
        var transactions = await _transactionRepository.GetTransactionsByAccountId(accountId);

        return transactions.Select(t => new TransactionResponse
            {
                TransactionId = t.Id,
                FromAccountId = t.FromAccountId,
                ToAccountId = t.ToAccountId,
                Amount = t.Amount,
                Currency = t.Currency,
                TransactionDate = t.TransactionDate
            }
        ).ToList();
    }
}