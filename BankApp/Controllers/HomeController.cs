using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers;

public class HomeController : Controller
{
    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }
}