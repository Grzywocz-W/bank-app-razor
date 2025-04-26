namespace BankApp.DTOs;

public record ClientResponse(
    string Login,
    List<AccountResponse> Accounts
);