using BankApp.Providers;
using BankApp.Services;
using BankApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers;

[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
[Authorize]
[Route("transaction")]
public class TransactionController : Controller
{
    private readonly TransactionService _transactionService;
    private readonly CurrentUserProvider _currentUserProvider;

    public TransactionController(
        TransactionService transactionService,
        CurrentUserProvider currentUserProvider
    )
    {
        _transactionService = transactionService;
        _currentUserProvider = currentUserProvider;
    }

    [HttpGet("{accountId:long}")]
    public async Task<IActionResult> Transactions(
        long accountId,
        int page = 1,
        int pageSize = 10
    )
    {
        var clientId = _currentUserProvider.GetClientId();

        if (!await _transactionService.IsAccountOwnedByClient(accountId, clientId))
            return Forbid();

        var (transactions, totalCount) = await _transactionService
            .GetPagedByAccountId(accountId, page, pageSize);

        var viewModel = new TransactionListViewModel(
            accountId,
            transactions,
            page,
            pageSize,
            totalCount
        );

        return View(viewModel);
    }
}