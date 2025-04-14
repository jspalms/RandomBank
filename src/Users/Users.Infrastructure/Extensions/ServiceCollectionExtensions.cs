using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Users.Domain.Interfaces;
using Users.Infrastructure.Data;
using Users.Infrastructure.Data.Repositories;
using Microsoft.Extensions.Configuration;
using ConfigurationManager = Microsoft.Extensions.Configuration.ConfigurationManager;
using Users.Infrastructure.Events.DomainEvents;
using Users.Application.Interfaces;
using Users.Infrastructure.Events.IntegrationEvents;

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
        services.AddKafkaProducer(configuration);
        services.AddHostedService<IntegrationEventConsumer>();
        
        return services;
    }
};