using BankApp.Services;
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
    public async Task<IActionResult> Transactions(long accountId)
    {
        try
        {
            var transactions = await _transactionService.GetByAccountId(accountId);
            ViewData["AccountId"] = accountId;
            return View(transactions);
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return RedirectToAction("MyAccounts", "Client");
        }
    }
}