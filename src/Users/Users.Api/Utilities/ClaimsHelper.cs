using System.Security.Claims;

namespace Users.Api.Utilities;

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

    public static string? GetFirstName(ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirst(ClaimTypes.GivenName)?.Value;
    }

    public static string? GetLastName(ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirst(ClaimTypes.Surname)?.Value;
    }
}