using Accounts.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Accounts.Infrastructure.Extensions;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddKeycloakAuthentication(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddOptions<KeycloakOptions>()
            .Bind(configuration.GetSection("Keycloak"))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        var keycloakOptions = configuration.GetSection("Keycloak").Get<KeycloakOptions>() ?? throw new Exception();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(jwtOptions =>
            {
                jwtOptions.Authority = keycloakOptions.Authority;
                jwtOptions.Audience = keycloakOptions.ClientId;
                jwtOptions.MetadataAddress = keycloakOptions.MetadataAddress;
                jwtOptions.RequireHttpsMetadata = false;
            });

        services.AddAuthorization();

        return services;
    }
}