using System.Security.Claims;
using BankApp.DTOs;
using BankApp.Helpers;
using BankApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers;

[Route("client")]
public class ClientController : Controller
{
    private readonly ClientService _clientService;
    private readonly UserHelper _userHelper;

    public ClientController(
        ClientService clientService,
        UserHelper userHelper
    )
    {
        _clientService = clientService;
        _userHelper = userHelper;
    }

    [HttpGet("register")]
    public IActionResult Register() => View(new RegisterRequest());

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
    public IActionResult Login() => View(new LoginRequest());

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromForm] LoginRequest loginRequest)
    {
        try
        {
            var client = await _clientService.Authenticate(
                loginRequest.Login,
                loginRequest.Password
            );

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, client.ClientId.ToString()),
                new Claim(ClaimTypes.Name, client.Login)
            };

            var identity = new ClaimsIdentity(claims, "MyCookieAuth");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("MyCookieAuth", principal);

            return RedirectToAction("Dashboard", "Client");
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return View(loginRequest);
        }
    }

    [Authorize]
    [HttpGet("dashboard")]
    public async Task<IActionResult> Dashboard()
    {
        var clientId = _userHelper.GetClientId();
        var client = await _clientService.FindById(clientId);

        return View(
            client.Accounts
                .OrderByDescending(a => a.AccountId)
                .ToList()
        );
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("MyCookieAuth");
        TempData["Success"] = "You have been logged out successfully.";
        
        return RedirectToAction("Index", "Home");
    }

    [Authorize]
    [HttpPost("delete")]
    public async Task<IActionResult> Delete()
    {
        var clientId = _userHelper.GetClientId();
        var client = await _clientService.FindById(clientId);

        try
        {
            await _clientService.RemoveByLogin(client.Login);
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