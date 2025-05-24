using BankApp.Helpers;
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
    private readonly UserHelper _userHelper;

    public TransactionController(
        TransactionService transactionService,
        UserHelper userHelper
    )
    {
        _transactionService = transactionService;
        _userHelper = userHelper;
    }

    [HttpGet("{accountId:long}")]
    public async Task<IActionResult> Transactions(
        long accountId,
        int page = 1,
        int pageSize = 10
    )
    {
        var clientId = _userHelper.GetClientId();

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