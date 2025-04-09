namespace BankApp.Models;

public class Client
{
    public long UserId { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public List<Account> Accounts { get; set; }

    public Client()
    {
        Accounts = new List<Account>();
    }

    public decimal GetBalance()
    {
        if (Accounts.Count != 0)
            return Accounts.First().Balance;
        return 0;
    }

    public void SetBalance(decimal newBalance)
    {
        if (Accounts.Count != 0)
            Accounts.First().Balance = newBalance;
    }
}