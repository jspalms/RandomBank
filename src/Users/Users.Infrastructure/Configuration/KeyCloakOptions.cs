﻿namespace Users.Infrastructure.Configuration;

public class KeycloakOptions
{
    public Uri AuthorisationUrl { get; set; }
    public Uri TokenUrl { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string Authority { get; set; }
    public string MetadataAddress { get; set; }
    public string Audience { get; init; } = "account";
}
