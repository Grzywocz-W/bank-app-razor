using System;
using System.Collections.Generic;

namespace BankApp.DTOs
{
    public class ClientResponse
    {
        public long Id { get; set; }
        public string Login { get; set; }  // Zmienione z Name na Login
        public string Password { get; set; }  // Dodano pole Password
        public List<long> Accounts { get; set; }

        public ClientResponse(long id, string login, List<long> accounts, string password)
        {
            Id = id;
            Login = login;
            Accounts = accounts;
            Password = password;
        }
    }
}