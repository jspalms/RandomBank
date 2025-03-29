using System.ComponentModel.DataAnnotations;

namespace Accounts.Infrastructure.Configuration;

public class KeycloakOptions
{
    [Required]
    public Uri AuthorisationUrl { get; set; }
    [Required]
    public Uri TokenUrl { get; set; }
    [Required]
    public string ClientId { get; set; }
    [Required]
    public string ClientSecret { get; set; }
    [Required]
    public string Authority { get; set; }
    [Required]
    public string MetadataAddress { get; set; }
}
