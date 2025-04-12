using BankApp.Data;
using BankApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Repositories;

public class ClientRepository
{
    private readonly ApplicationDbContext _context;

    public ClientRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task SaveAsync(Client client)
    {
        await _context.Clients.AddAsync(client);
        await _context.SaveChangesAsync();
    }

    public async Task<Client> FindByLoginAsync(string login)
    {
        return await _context.Clients
            .Include(c => c.Accounts)
            .FirstOrDefaultAsync(c => c.Login == login);
    }

    public async Task<Client> FindByIdAsync(long clientId)
    {
        return await _context.Clients
            .Include(c => c.Accounts)
            .FirstOrDefaultAsync(c => c.ClientId == clientId);
    }

    public async Task DeleteAsync(Client client)
    {
        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
    }
}