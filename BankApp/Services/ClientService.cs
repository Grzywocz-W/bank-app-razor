using BankApp.DTOs;
using BankApp.Models;
using BankApp.Repositories;

public class ClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly ILogger<ClientService> _logger;

    public ClientService(IClientRepository clientRepository, ILogger<ClientService> logger)
    {
        _clientRepository = clientRepository;
        _logger = logger;
    }

    // Asynchroniczna metoda do zapisu nowego klienta
    public void  Save(ClientRequest clientRequest)
    {
        var client = new Client
        {
            // UserId = Guid.NewGuid(),  // Używamy unikalnego identyfikatora dla użytkownika
            Login = clientRequest.Login,
            Password = clientRequest.Password  // Pamiętaj, żeby w prawdziwej aplikacji hasło było haszowane
        };

        // Dodajemy klienta do repozytorium
         _clientRepository.Save(client);
    }

    public ClientResponse FindResponseByEmail(string login)
    {
        var client = _clientRepository.FindByLogin(login);  // Używamy Login zamiast Email
        if (client == null)
            return null;

        return new ClientResponse(client.UserId, client.Login, client.Accounts.Select(a => a.AccountId).ToList(),
            client.Password);
    }

    public void RemoveByLogin(string login)
    {
        var client = _clientRepository.FindByLogin(login);  // Używamy Login zamiast Email
        if (client == null)
        {
            throw new Exception("Client not found");
        }

        if (client.GetBalance() > 0)
        {
            throw new UnauthorizedAccessException("Client balance must be 0 to delete.");
        }

        _clientRepository.Delete(client);
    }
}