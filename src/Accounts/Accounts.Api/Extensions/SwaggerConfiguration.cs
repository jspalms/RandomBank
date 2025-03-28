using Accounts.Infrastructure.Configuration;

namespace Accounts.Api.Extensions;

using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

public static class SwaggerConfiguration
{
    public static IServiceCollection AddConfiguredSwagger(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var keycloakOptions = serviceProvider.GetRequiredService<IOptions<KeycloakOptions>>().Value;

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

            // OAuth2 configuration
            c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = keycloakOptions.AuthorisationUrl,
                        TokenUrl = keycloakOptions.TokenUrl,
                        Scopes = new Dictionary<string, string>
                        {
                            { "openid", "OpenID Connect scope" },
                            { "profile", "Profile information" }
                        }
                    }
                }
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "oauth2"

                        },
                        In =  ParameterLocation.Header
                    },
                    new[] { "openid", "profile" }
                }
        });
        });
        return services;
    }
}