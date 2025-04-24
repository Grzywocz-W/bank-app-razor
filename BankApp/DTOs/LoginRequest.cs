using System.ComponentModel.DataAnnotations;

namespace BankApp.DTOs;

public class LoginRequest
{
    [Required] public string Login { get; init; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; init; }
}