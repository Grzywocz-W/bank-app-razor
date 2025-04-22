using BankApp.DTOs;
using BankApp.Models;
using BankApp.Repositories;

namespace BankApp.Services;

public class TransactionService
{
    private readonly TransactionRepository _transactionRepository;

    public TransactionService(TransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task Save(TransactionRequest request)
    {
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
        var transactions = await _transactionRepository
            .GetTransactionsByAccountIdAsync(accountId);

        return transactions.Select(t => new TransactionResponse
            {
                FromAccountId = t.FromAccountId,
                ToAccountId = t.ToAccountId,
                Amount = t.Amount,
                Currency = t.Currency,
                TransactionDate = t.TransactionDate
            }
        ).ToList();
    }
}