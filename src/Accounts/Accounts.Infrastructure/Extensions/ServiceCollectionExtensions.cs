namespace Accounts.Infrastructure.Extensions;

using Accounts.Infrastructure.Configuration;
using Data;
using Data.Repositories;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Events;

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
        services.AddKeycloakAuthentication(configuration);
        
        return services;
    }
}