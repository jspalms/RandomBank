using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Users.Infrastructure.Configuration;

namespace Users.Infrastructure.Extensions;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddKeycloakAuthentication(this IServiceCollection services, KeycloakOptions keycloakOptions)
    {
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