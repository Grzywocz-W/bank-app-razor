using System.ComponentModel.DataAnnotations;

namespace BankApp.DTOs;

public class RegisterRequest
{
    [Required] public string Login { get; init; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; init; }

    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; init; }
}