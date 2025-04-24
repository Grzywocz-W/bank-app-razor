namespace BankApp.DTOs;

public class ClientResponse
{
    public string Login { get; init; }

    public List<AccountResponse> Accounts { get; init; }
}