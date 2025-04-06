using System;
using BankApp.Models;

namespace BankApp.Repositories
{
    public interface IClientRepository
    {
        Client FindByLogin(string login);  // Zmienione z email na login
        void Save(Client client);
        void Delete(Client client);
    }
}