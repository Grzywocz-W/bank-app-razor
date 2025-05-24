using System.Security.Claims;

namespace BankApp.Helpers;

public class UserHelper
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserHelper(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public long GetClientId()
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null)
            throw new UnauthorizedAccessException("User is not authenticated.");

        var claim = user.FindFirst(ClaimTypes.NameIdentifier);
        if (claim == null)
            throw new UnauthorizedAccessException("User is not authenticated.");

        if (!long.TryParse(claim.Value, out var clientId))
            throw new UnauthorizedAccessException("Invalid client ID claim.");

        return clientId;
    }
}