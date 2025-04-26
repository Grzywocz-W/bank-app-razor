using AutoMapper;
using BankApp.DTOs;
using BankApp.Models;
using BankApp.Repositories;

namespace BankApp.Services;

public class TransactionService
{
    private readonly TransactionRepository _transactionRepository;
    private readonly IMapper _mapper;

    public TransactionService(
        TransactionRepository transactionRepository,
        IMapper mapper
    )
    {
        _transactionRepository = transactionRepository;
        _mapper = mapper;
    }

    public async Task Save(TransactionRequest transactionRequest)
    {
        var transaction = _mapper.Map<Transaction>(transactionRequest);

        await _transactionRepository.SaveAsync(transaction);
    }

    public async Task<(List<TransactionResponse> Transactions, int TotalCount)> GetPagedByAccountId(
        long accountId,
        int page,
        int pageSize
    )
    {
        var transactions = await _transactionRepository
            .GetTransactionsByAccountIdAsync(accountId);
        var totalCount = transactions.Count;

        var paged = transactions
            .OrderByDescending(t => t.TransactionDate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(t => _mapper.Map<TransactionResponse>(t))
            .ToList();

        return (paged, totalCount);
    }

    public async Task<bool> IsAccountOwnedByClient(
        long accountId,
        long clientId
    )
    {
        return await _transactionRepository.IsAccountOwnedByClientAsync(accountId, clientId);
    }
}