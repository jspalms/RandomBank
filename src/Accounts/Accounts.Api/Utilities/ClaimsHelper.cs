namespace Accounts.Api.Utilities;

using System.Security.Claims;

public static class ClaimsHelper
{
    public static Guid? GetUserId(ClaimsPrincipal claimsPrincipal)
    {
        var userIdString = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (Guid.TryParse(userIdString, out var userId))
        {
            return userId;
        }
        return null;
    }

    public static string? GetEmail(ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value;
    }
}
