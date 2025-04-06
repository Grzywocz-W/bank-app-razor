namespace BankApp.Models
{
    public class Client
    {
        public long UserId { get; set; }
        public string Login { get; set; }  // Zmienione z Name na Login
        public string Password { get; set; }  // Dodano pole Password
        public List<Account> Accounts { get; set; }

        public Client()
        {
            Accounts = new List<Account>();  // Zapewniamy, że Accounts będzie zawsze listą
        }

        public double GetBalance()
        {
            if (Accounts != null && Accounts.Any())
                return Accounts.First().Balance;
            return 0;
        }

        public void SetBalance(double newBalance)
        {
            if (Accounts != null && Accounts.Any())
                Accounts.First().Balance = newBalance;
        }

        public override string ToString()
        {
            return $"Client{{ id={UserId}, login='{Login}', password='{Password}', accounts={Accounts.Count} }}";
        }
    }
}