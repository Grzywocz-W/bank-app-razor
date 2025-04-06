namespace BankApp.DTOs
{
    public class ClientRequest
    {
        public string Login { get; set; }  // Zmienione z Name na Login
        public long UserId { get; set; }  // Zmienione z Name na Login
        public string Password { get; set; }  // Dodano pole Password
    }
}