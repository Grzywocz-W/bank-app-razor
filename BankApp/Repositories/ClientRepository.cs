using BankApp.Data;
using BankApp.Models;

namespace BankApp.Repositories;

public interface IClientRepository
{
    Client FindByLogin(string login);
    void Save(Client client);
    void Delete(Client client);
}

public class ClientRepository : IClientRepository
{
    private readonly ApplicationDbContext _context;

    public ClientRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // Asynchroniczna metoda do zapisu klienta
    public async void Save(Client client)
    {
        await _context.Clients.AddAsync(client);
        await _context.SaveChangesAsync();
    }

    public Client FindByLogin(string login)
    {
        return _context.Clients.FirstOrDefault(c => c.Login == login);
    }

    public void Delete(Client client)
    {
        _context.Clients.Remove(client);
        _context.SaveChanges();
    }
}