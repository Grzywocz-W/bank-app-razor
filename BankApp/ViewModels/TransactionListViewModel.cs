using BankApp.DTOs;

namespace BankApp.ViewModels;

public class TransactionListViewModel
{
    public long AccountId { get; private set; }
    public List<TransactionResponse> Transactions { get; private set; }
    public int CurrentPage { get; private set; }
    public int PageSize { get; private set; }
    public int TotalCount { get; private set; }

    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

    public TransactionListViewModel(
        long accountId,
        List<TransactionResponse> transactions,
        int currentPage,
        int pageSize,
        int totalCount
    )
    {
        AccountId = accountId;
        Transactions = transactions;
        CurrentPage = currentPage;
        PageSize = pageSize;
        TotalCount = totalCount;
    }
}