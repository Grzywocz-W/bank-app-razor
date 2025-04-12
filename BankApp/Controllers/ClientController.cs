using BankApp.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers;

public class ClientController : Controller
{
    private readonly ClientService _clientService;

    public ClientController(ClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet("register")]
    public IActionResult Register() => View();

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromForm] ClientRequest clientRequest)
    {
        try
        {
            await _clientService.SaveAsync(clientRequest);
            return RedirectToAction("Login");
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error: {ex.Message}";
            return View();
        }
    }

    [HttpGet("login")]
    public IActionResult Login() => View();

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromForm] ClientRequest clientRequest)
    {
        var client = await _clientService.FindByLoginAsync(clientRequest.Login);
        if (client.Password != clientRequest.Password)
        {
            TempData["Error"] = "Invalid login credentials.";
            return View();
        }

        HttpContext.Session.SetString("ClientId", client.ClientId.ToString());
        return RedirectToAction("MyAccounts", "Account");
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Remove("ClientId");
        return RedirectToAction("Login");
    }
}