public class AccountResponse
{
    public long Id { get; set; }
    public double Balance { get; set; }
    public string Currency { get; set; }
    public long UserId { get; set; }
    public string AccountNumber { get; set; }  // Dodajemy numer konta
}