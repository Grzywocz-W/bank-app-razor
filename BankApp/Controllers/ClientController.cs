using BankApp.DTOs;
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

    [HttpGet("register")]
    public IActionResult Register() => View();

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromForm] RegisterRequest registerRequest)
    {
        if (!ModelState.IsValid)
        {
            TempData["Error"] = "Invalid login or passwords.";
            return View(registerRequest);
        }

        try
        {
            await _clientService.Save(registerRequest);
            return RedirectToAction("Login");
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return View(registerRequest);
        }
    }

    [HttpGet("login")]
    public IActionResult Login() => View();

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromForm] LoginRequest loginRequest)
    {
        try
        {
            var client = await _clientService.Authenticate(
                loginRequest.Login,
                loginRequest.Password
            );

            HttpContext.Session.SetString("ClientId", client.ClientId.ToString());
            return RedirectToAction("Dashboard", "Client");
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return View(loginRequest);
        }
    }

    [HttpGet("dashboard")]
    public async Task<IActionResult> Dashboard()
    {
        var clientIdString = HttpContext.Session.GetString("ClientId");
        if (!long.TryParse(clientIdString, out var clientId))
            return RedirectToAction("Index", "Home");

        var client = await _clientService.FindById(clientId);
        ViewData["ClientId"] = clientId;

        return View(
            client.Accounts
                .OrderByDescending(a => a.AccountId)
                .ToList()
        );
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Remove("ClientId");
        TempData["Success"] = "You have been logged out successfully.";
        return RedirectToAction("Index", "Home");
    }

    [HttpPost("client/delete")]
    public async Task<IActionResult> Delete()
    {
        var clientIdString = HttpContext.Session.GetString("ClientId");
        if (!long.TryParse(clientIdString, out var clientId))
        {
            TempData["Error"] = "You must be logged in to delete your account.";
            return RedirectToAction("Login", "Client");
        }

        var client = await _clientService.FindById(clientId);

        try
        {
            await _clientService.RemoveByLogin(client.Login);
            HttpContext.Session.Remove("ClientId");
            TempData["Success"] = "Client account deleted successfully.";
            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return RedirectToAction("Dashboard", "Client");
        }
    }
}