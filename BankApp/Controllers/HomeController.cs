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
    public async Task<IActionResult> Login([FromForm] ClientRequest clientRequest)
    {
        ClientResponse client;

        try
        {
            client = await _clientService.FindByLogin(clientRequest.Login);
        }
        catch (ArgumentException ex)
        {
            TempData["Error"] = ex.Message;
            return View(clientRequest);
        }

        if (client.Password != clientRequest.Password)
        {
            TempData["Error"] = "Invalid login credentials.";
            return View();
        }

        HttpContext.Session.SetString("ClientId", client.ClientId.ToString());
        return RedirectToAction("MyAccounts", "Client");
    }

    [HttpGet("register")]
    public IActionResult Register() => View();

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromForm] ClientRequest clientRequest)
    {
        if (!ModelState.IsValid)
        {
            TempData["Error"] = "Passwords do not match.";
            return View(clientRequest);
        }

        try
        {
            await _clientService.Save(clientRequest);
            return RedirectToAction("Login");
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return View(clientRequest);
        }
    }
}