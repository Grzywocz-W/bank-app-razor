using BankApp.DTOs;
using BankApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers;

public class AccountController : Controller
{
    private readonly AccountService _accountService;

    public AccountController(AccountService accountService)
    {
        _accountService = accountService;
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
        var clientId = HttpContext.Session.GetString("ClientId");
        if (clientId == null)
        {
            TempData["Error"] = "You must be logged in to perform this action.";
            return RedirectToAction("Login", "Home");
        }

        try
        {
            await _accountService.Transfer(fromId, toId, amount);
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
            await _accountService.Withdraw(id, amount);
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