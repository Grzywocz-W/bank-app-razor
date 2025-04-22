namespace BankApp.DTOs;

public class ClientResponse
{
    public long ClientId { get; init; }

    public string Login { get; init; }

    public string Password { get; init; }

    public List<AccountResponse> Accounts { get; init; }
}