namespace BankApp.DTOs;

public class ClientRequest
{
    public long UserId { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }

    // public ClientRequest(string login, string password)
    // {
    //     Login = login;
    //     Password = password;
    // }
}