using BankApp.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BankApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ClientService _clientService;

        public HomeController(ClientService clientService)
        {
            _clientService = clientService;
        }

        // Main landing page
        [HttpGet("")]
        [HttpGet("home")]
        public IActionResult Index()
        {
            return View();
        }

        // Login page
        [HttpGet("home/login")]
        public IActionResult Login() => View();

        [HttpPost("home/login")]
        public async Task<IActionResult> Login([FromForm] ClientRequest clientRequest)
        {
            var client = await _clientService.FindByLoginAsync(clientRequest.Login);
            if (client == null || client.Password != clientRequest.Password)
            {
                TempData["Error"] = "Invalid login credentials.";
                return View();
            }

            HttpContext.Session.SetString("ClientId", client.ClientId.ToString());
            return RedirectToAction("MyAccounts", "Client"); // Redirect to your account page or dashboard
        }

        // Register page
        [HttpGet("home/register")]
        public IActionResult Register() => View();

        [HttpPost("home/register")]
        public async Task<IActionResult> Register([FromForm] ClientRequest clientRequest)
        {
            try
            {
                await _clientService.SaveAsync(clientRequest); // Assuming SaveAsync creates a new user
                return RedirectToAction("Login"); // After successful registration, redirect to login
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error: {ex.Message}";
                return View();
            }
        }
    }
}