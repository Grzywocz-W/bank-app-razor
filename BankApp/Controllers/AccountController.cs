using BankApp.DTOs;
using BankApp.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BankApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _service;

        public AccountController(AccountService service)
        {
            _service = service;
        }

        // Endpoint do tworzenia konta
        [HttpPost]
        public IActionResult CreateAccount([FromBody] AccountRequest account)
        {
            try
            {
                _service.Save(account);
                return StatusCode(201); // Status 201 - Created
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        // Endpoint do wyszukiwania konta po ID
        [HttpGet]
        public IActionResult GetAccountById([FromQuery] long id)
        {
            try
            {
                var account = _service.FindById(id);
                return Ok(account);  // Status 200 - OK
            }
            catch (Exception ex)
            {
                return NotFound($"Error: {ex.Message}"); // Status 404 - Not Found
            }
        }

        // Endpoint do przelewu między kontami
        [HttpPost("transfer")]
        public IActionResult Transfer([FromQuery] long fromId, [FromQuery] long toId, [FromQuery] double amount)
        {
            try
            {
                _service.Transfer(fromId, toId, amount);
                return NoContent();  // Status 204 - No Content
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}"); // Status 400 - Bad Request
            }
        }

        // Endpoint do wypłaty z konta
        [HttpPost("withdraw")]
        public IActionResult Withdraw([FromQuery] long id, [FromQuery] double amount)
        {
            try
            {
                _service.WithDraw(id, amount);
                return NoContent();  // Status 204 - No Content
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}"); // Status 400 - Bad Request
            }
        }
    }
}
