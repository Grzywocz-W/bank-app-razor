using Microsoft.AspNetCore.Mvc;
using BankApp.Services;
using BankApp.DTOs;

namespace BankApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ClientService _clientService;

        public HomeController(ClientService clientService)
        {
            _clientService = clientService;
        }

        // Akcja GET, która zwraca widok logowania
        public IActionResult Login()
        {
            return View();
        }

        // Akcja POST, która przyjmuje dane logowania z formularza
        [HttpPost]
        public IActionResult Login(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Login i hasło są wymagane.");
                return View();
            }

            var clientResponse = _clientService.FindResponseByEmail(login);
            if (clientResponse == null || clientResponse.Password != password)
            {
                ViewData["ErrorMessage"] = "Niepoprawny login lub hasło.";
                return View();
            }

            // Logowanie powiodło się
            return RedirectToAction("Index", "Home");
        }

        // Akcja GET, która zwraca widok rejestracji
        public IActionResult Register()
        {
            return View();
        }

        // Akcja POST, która przyjmuje dane rejestracji z formularza
        [HttpPost]
        public IActionResult Register(ClientRequest clientRequest)
        {
            if (ModelState.IsValid)
            {
                _clientService.Save(clientRequest); // Rejestracja nowego klienta
                return RedirectToAction("Login"); // Po udanej rejestracji, przejście do logowania
            }

            return View();
        }
    }
}