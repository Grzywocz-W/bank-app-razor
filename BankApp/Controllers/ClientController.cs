using BankApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers;

public class ClientController : Controller
{
    private readonly ClientService _clientService;

    public ClientController(ClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet("myaccounts")]
    public async Task<IActionResult> MyAccounts()
    {
        var clientId = HttpContext.Session.GetString("ClientId");
        if (clientId == null)
            return RedirectToAction("", "Home");

        var client = await _clientService.FindById(long.Parse(clientId));
        ViewData["ClientId"] = clientId;

        return View(
            client.Accounts
                .OrderByDescending(a => a.AccountId)
                .ToList()
        );
    }

    [HttpPost("client/delete")]
    public async Task<IActionResult> Delete()
    {
        var clientId = HttpContext.Session.GetString("ClientId");

        if (string.IsNullOrEmpty(clientId))
        {
            TempData["Error"] = "You must be logged in to delete your account.";
            return RedirectToAction("Login", "Home");
        }

        var client = await _clientService.FindById(long.Parse(clientId));

        try
        {
            await _clientService.RemoveByLogin(client.Login);
            HttpContext.Session.Remove("ClientId");
            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return RedirectToAction("MyAccounts", "Client");
        }
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Remove("ClientId");
        return RedirectToAction("Index", "Home");
    }
}