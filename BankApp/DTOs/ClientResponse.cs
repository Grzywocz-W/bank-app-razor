namespace BankApp.DTOs;

public class ClientResponse
{
    public long ClientId { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public List<AccountResponse> Accounts { get; set; }

    public ClientResponse(long clientId, string login, string password, List<AccountResponse> accounts)
    {
        ClientId = clientId;
        Login = login;
        Password = password;
        Accounts = accounts;
    }
}