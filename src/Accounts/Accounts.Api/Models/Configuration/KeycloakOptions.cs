namespace Accounts.Api.Models.Configuration;

public class KeycloakOptions
{
    public Uri AuthorisationUrl { get; set; }
    public Uri TokenUrl { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
}
