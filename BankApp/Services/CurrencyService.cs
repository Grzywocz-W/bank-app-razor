using BankApp.DTOs;

namespace BankApp.Services;

public class CurrencyService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<CurrencyService> _logger;

    public CurrencyService(HttpClient httpClient, ILogger<CurrencyService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<CurrencyResponse> GetCurrencyRatesAsync()
    {
        const string apiUrl =
            "https://api.exchangeratesapi.io/v1/latest?access_key=9e469967cdbce475fa4242cfe3d8e392&format=1";

        try
        {
            var response = await _httpClient.GetFromJsonAsync<CurrencyResponse>(apiUrl);
            return response!;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Error fetching currency rates");
            return null!;
        }
    }
}