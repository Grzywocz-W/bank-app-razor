using BankApp.DTOs;
using BankApp.Helpers;
using BankApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers;

[Authorize]
[Route("account")]
public class AccountController : Controller
{
    private readonly AccountService _accountService;
    private readonly UserHelper _userHelper;

    public AccountController(
        AccountService accountService,
        UserHelper userHelper
    )
    {
        _accountService = accountService;
        _userHelper = userHelper;
    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromForm] AccountRequest accountRequest)
    {
        var clientId = _userHelper.GetClientId();
        accountRequest.ClientId = clientId;

        try
        {
            await _accountService.Save(accountRequest);
            TempData["Success"] = "New account has been successfully created.";
            return RedirectToAction("Dashboard", "Client");
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
            await _accountService.Transfer(fromId, toId, amount);
            TempData["Success"] = "Transfer completed successfully.";
            return RedirectToAction("Dashboard", "Client");
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return RedirectToAction("Dashboard", "Client");
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
            TempData["Success"] = "Withdrawal completed successfully.";
            return RedirectToAction("Dashboard", "Client");
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return RedirectToAction("Dashboard", "Client");
        }
    }

    [HttpPost("delete")]
    public async Task<IActionResult> Delete([FromForm] long accountId)
    {
        try
        {
            await _accountService.Delete(accountId);
            TempData["Success"] = "Account deleted successfully.";
            return RedirectToAction("Dashboard", "Client");
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return RedirectToAction("Dashboard", "Client");
        }
    }
}