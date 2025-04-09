namespace BankApp.DTOs;

public class ClientResponse
{
    public long Id { get; set; }
    public string Login { get; set; } 
    public string Password { get; set; } 
    public List<long> Accounts { get; set; }

    public ClientResponse(long id, string login, List<long> accounts, string password)
    {
        Id = id;
        Login = login;
        Accounts = accounts;
        Password = password;
    }
}