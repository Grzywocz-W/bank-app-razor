using BankApp.DTOs;
using BankApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers;

public class HomeController : Controller
{
    private readonly ClientService _clientService;

    public HomeController(ClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
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

    [HttpGet("register")]
    public IActionResult Register() => View();

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromForm] RegisterRequest registerRequest)
    {
        if (!ModelState.IsValid)
        {
            TempData["Error"] = "Passwords do not match.";
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
}