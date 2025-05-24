using BankApp.Models;

namespace BankApp.Validators;

public class ClientValidator
{
    public void ValidateClient(Client? client)
    {
        if (client == null)
            throw new ArgumentException("Client not found.");
    }
}