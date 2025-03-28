using System.Configuration;
using Accounts.Infrastructure.Configuration;

namespace Accounts.Infrastructure.Extensions;

using Data;
using Data.Repositories;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Accounts.Infrastructure.Events;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("PostgresConnection")));
        services.AddScoped<IPortfolioRepository, PortfolioRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddSingleton<IEventPublisher, EventPublisher>();
        services.AddHostedService<OutboxProcessor>();
        services.Configure<KeycloakOptions>(configuration.GetSection("Keycloak"));
        var keycloakOptions = configuration.GetSection("Keycloak").Get<KeycloakOptions>() ?? throw new ConfigurationErrorsException();
        services.AddKeycloakAuthentication(keycloakOptions);
        
        return services;
    }
}