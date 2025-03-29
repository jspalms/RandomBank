using System.Configuration;
using Accounts.Infrastructure.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Users.Domain.Interfaces;
using Users.Infrastructure.Configuration;
using Users.Infrastructure.Data;
using Users.Infrastructure.Data.Repositories;
using Users.Infrastructure.Events;
using Microsoft.Extensions.Configuration;
using ConfigurationManager = Microsoft.Extensions.Configuration.ConfigurationManager;

namespace Users.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("PostgresConnection")));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddSingleton<IEventPublisher, EventPublisher>();
        services.AddHostedService<OutboxProcessor>();
        services.AddKeycloakAuthentication(configuration);
        
        return services;
    }
}