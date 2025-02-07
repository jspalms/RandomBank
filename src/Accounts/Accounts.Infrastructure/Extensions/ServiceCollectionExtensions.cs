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
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddSingleton<IEventPublisher, EventPublisher>();
        services.AddHostedService<OutboxProcessor>();
        
        return services;
    }
}