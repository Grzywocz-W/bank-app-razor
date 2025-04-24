namespace BankApp.Security;

public class PasswordHasher
{
    public static string Hash(string password) 
        => BCrypt.Net.BCrypt.HashPassword(password);
    
    public static bool Verify(string password, string hashed)
        => BCrypt.Net.BCrypt.Verify(password, hashed);
}