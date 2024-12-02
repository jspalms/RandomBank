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
        services.AddScoped<IEventPublisher<AccountOpenedIntegrationEvent>, EventPublisher<AccountOpenedIntegrationEvent>>();

        services.AddMassTransit(x =>
        {
            x.AddRider(rider =>
            {
                rider.AddProducer<AccountOpenedIntegrationEvent>("topic-name");
                rider.UsingKafka((context, k) => { k.Host("localhost:9092"); });
            });
            x.AddEntityFrameworkOutbox<ApplicationDbContext>(o =>
            {
                o.QueryDelay = TimeSpan.FromSeconds(1);
                o.UsePostgres();
                o.UseBusOutbox();
            });
        });

        return services;
    }
}