using BankApp.DTOs;
using BankApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers;

public class ClientController : Controller
{
    private readonly ClientService _clientService;
    private readonly ILogger<ClientController> _logger;

    public ClientController(ClientService clientService, ILogger<ClientController> logger)
    {
        _clientService = clientService;
        _logger = logger;
    }

    // Akcja POST do tworzenia nowego klienta
    [HttpPost]
    public IActionResult CreateClient(string login, string password)
    {
        try
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Login i hasło są wymagane.");
                return View();
            }

            var clientRequest = new ClientRequest
            {
                Login = login,
                Password = password
            };

            _clientService.Save(clientRequest);

            ViewData["SuccessMessage"] = "Klient został pomyślnie utworzony!";
            return View();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Błąd podczas tworzenia klienta: {ex.Message}");
            ViewData["ErrorMessage"] = "Nie udało się utworzyć klienta.";
            return View();
        }
    }
}