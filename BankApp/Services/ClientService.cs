using BankApp.DTOs;
using BankApp.Models;
using BankApp.Repositories;

namespace BankApp.Services;

public class ClientService
{
    private readonly ClientRepository _clientRepository;

    public ClientService(ClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<ClientResponse> FindByIdAsync(long clientId)
    {
        var client = await _clientRepository.FindByIdAsync(clientId);
        if (client == null)
            throw new ArgumentException("Client not found.");

        var accounts = client.Accounts.Select(a => new AccountResponse
        {
            AccountId = a.AccountId,
            Balance = a.Balance,
            Currency = a.Currency,
            ClientId = a.ClientId
        }).ToList();

        return new ClientResponse(
            client.ClientId,
            client.Login,
            client.Password, accounts
        );
    }

    public async Task<ClientResponse> FindByLoginAsync(string login)
    {
        var client = await _clientRepository.FindByLoginAsync(login);
        if (client == null)
            throw new ArgumentException("Client not found.");

        var accounts = client.Accounts.Select(a => new AccountResponse
        {
            AccountId = a.AccountId,
            Balance = a.Balance,
            Currency = a.Currency,
            ClientId = a.ClientId
        }).ToList();

        return new ClientResponse(
            client.ClientId,
            client.Login,
            client.Password, accounts
        );
    }

    public async Task SaveAsync(ClientRequest clientRequest)
    {
        var client = new Client
        {
            Login = clientRequest.Login,
            Password = clientRequest.Password
        };

        await _clientRepository.SaveAsync(client);
    }

    public async Task RemoveByLoginAsync(string login)
    {
        var client = await _clientRepository.FindByLoginAsync(login);
        if (client == null)
            throw new ArgumentException("Client not found.");

        if (client.GetBalance() > 0)
            throw new InvalidOperationException("Client balance must be zero before deletion.");

        await _clientRepository.DeleteAsync(client);
    }
}