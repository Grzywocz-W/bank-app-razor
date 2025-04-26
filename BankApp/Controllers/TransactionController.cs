using BankApp.Services;
using BankApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers;

public class TransactionController : Controller
{
    private readonly TransactionService _transactionService;

    public TransactionController(TransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpGet("transactions/{accountId:long}")]
    public async Task<IActionResult> Transactions(long accountId, int page = 1, int pageSize = 10)
    {
        var clientIdString = HttpContext.Session.GetString("ClientId");
        if (!long.TryParse(clientIdString, out var clientId))
            return RedirectToAction("Index", "Home");

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