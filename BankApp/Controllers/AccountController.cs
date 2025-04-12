using BankApp.DTOs;
using BankApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers;

public class AccountController : Controller
{
    private readonly AccountService _service;
    private readonly ClientService _clientService;

    public AccountController(AccountService service, ClientService clientService)
    {
        _service = service;
        _clientService = clientService;
    }

    [HttpGet("create")]
    public IActionResult CreateAccount()
    {
        return View();
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAccount([FromForm] AccountRequest accountRequest)
    {
        try
        {
            var clientId = HttpContext.Session.GetString("ClientId");
            if (clientId == null)
            {
                TempData["Error"] = "You must be logged in to create an account.";
                return RedirectToAction("Login", "Client");
            }

            accountRequest.ClientId = long.Parse(clientId);

            await _service.SaveAsync(accountRequest);
            return RedirectToAction("MyAccounts");
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error: {ex.Message}";
            return View();
        }
    }

    [HttpGet("myaccounts")]
    public async Task<IActionResult> MyAccounts()
    {
        var clientId = HttpContext.Session.GetString("ClientId");
        if (clientId == null)
            return RedirectToAction("Login", "Client");

        var client = await _clientService.FindByIdAsync(long.Parse(clientId));
        ViewData["ClientId"] = clientId;
        
        return View(client.Accounts);
    } 
  
    [HttpPost("transfer")]
    public async Task<IActionResult> Transfer([FromForm] long fromId, [FromForm] long toId, [FromForm] decimal amount)
    {
        var clientId = HttpContext.Session.GetString("ClientId");
        if (clientId == null)
        {
            TempData["Error"] = "You must be logged in to perform this action.";
            return RedirectToAction("Login", "Client");
        }

        try
        {
            await _service.TransferAsync(fromId, toId, amount);
            return RedirectToAction("MyAccounts");
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error: {ex.Message}";
            return RedirectToAction("MyAccounts");
        }
    }

    [HttpPost("withdraw")]
    public async Task<IActionResult> Withdraw([FromForm] long id, [FromForm] decimal amount)
    {
        try
        {
            await _service.WithdrawAsync(id, amount);
            return RedirectToAction("MyAccounts");
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error: {ex.Message}";
            return RedirectToAction("MyAccounts");
        }
    }

    [HttpPost("delete")]
    public async Task<IActionResult> DeleteAccount([FromForm] long accountId)
    {
        try
        {
            await _service.DeleteAsync(accountId);
            return RedirectToAction("MyAccounts");
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error: {ex.Message}";
            return RedirectToAction("MyAccounts");
        }
    }
}