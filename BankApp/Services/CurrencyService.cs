using BankApp.DTOs;

namespace BankApp.Services;

public class CurrencyService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<CurrencyService> _logger;
    // to get value from appsettings.json
    private readonly IConfiguration _configuration;

    public CurrencyService(
        HttpClient httpClient,
        ILogger<CurrencyService> logger,
        IConfiguration configuration
    )
    {
        _httpClient = httpClient;
        _logger = logger;
        _configuration = configuration;
    }

    public async Task<CurrencyResponse?> GetCurrencyRatesAsync()
    {
        // base currency is eur
        var apiUrl = _configuration["API_URL"];
        var accessKey = _configuration["CURRENCY_API_KEY"];
        if (string.IsNullOrEmpty(apiUrl))
        {
            _logger.LogError("API_URL is not set.");
            return null;
        }

        if (string.IsNullOrEmpty(accessKey))
        {
            _logger.LogError("CURRENCY_API_KEY is not set.");
            return null;
        }

        var url = $"{apiUrl}?access_key={accessKey}&format=1";

        try
        {
            var response = await _httpClient.GetFromJsonAsync<CurrencyResponse>(url);
            return response;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Error fetching currency rates");
            return null;
        }
    }
}