using BankApp.DTOs;
using BankApp.Models;
using BankApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers;

public class AccountController : Controller
{
    private readonly AccountService _accountService;
    private readonly TransactionService _transactionService;

    public AccountController(AccountService accountService, TransactionService transactionService)
    {
        _accountService = accountService;
        _transactionService = transactionService;
    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromForm] AccountRequest accountRequest)
    {
        try
        {
            var clientId = HttpContext.Session.GetString("ClientId");
            if (clientId == null)
            {
                TempData["Error"] = "You must be logged in to create an account.";
                return RedirectToAction("Login", "Home");
            }

            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Invalid balance.";
                return View(accountRequest);
            }

            accountRequest.ClientId = long.Parse(clientId);

            await _accountService.Save(accountRequest);
            return RedirectToAction("MyAccounts", "Client");
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return View();
        }
    }

    [HttpPost("transfer")]
    public async Task<IActionResult> Transfer(
        [FromForm] long fromId,
        [FromForm] long toId,
        [FromForm] decimal amount
    )
    {
        try
        {
            await _transactionService.Save(new TransactionRequest
            {
                FromAccountId = fromId,
                ToAccountId = toId,
                Amount = amount,
                Currency = Currency.PLN // możesz pobrać z bazy konta albo z formularza
            });

            TempData["Success"] = "Transfer completed successfully.";
            return RedirectToAction("MyAccounts", "Client");
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return RedirectToAction("MyAccounts", "Client");
        }
    }

    [HttpPost("withdraw")]
    public async Task<IActionResult> Withdraw(
        [FromForm] long id,
        [FromForm] decimal amount
    )
    {
        try
        {
            await _transactionService.Save(new TransactionRequest
            {
                FromAccountId = id,
                ToAccountId = null,
                Amount = amount,
                Currency = Currency.PLN // j.w.
            });

            TempData["Success"] = "Withdrawal completed successfully.";
            return RedirectToAction("MyAccounts", "Client");
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return RedirectToAction("MyAccounts", "Client");
        }
    }

    [HttpPost("account/delete")]
    public async Task<IActionResult> Delete([FromForm] long accountId)
    {
        try
        {
            await _accountService.Delete(accountId);
            return RedirectToAction("MyAccounts", "Client");
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return RedirectToAction("MyAccounts", "Client");
        }
    }
}