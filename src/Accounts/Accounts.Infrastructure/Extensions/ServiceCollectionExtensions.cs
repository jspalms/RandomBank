namespace Accounts.Infrastructure.Extensions;

using Data;
using Data.Repositories;
using Domain.IntegrationEvents;
using Domain.Interfaces;
using Events;
using MassTransit;
using MassTransit.KafkaIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));
        services.AddScoped<IAccountRepository, AccountRepository>();
        // services.AddScoped<IEventPublisher<AccountOpenedIntegrationEvent>, EventPublisher<AccountOpenedIntegrationEvent>>();
        
        
        return services;
    }
}