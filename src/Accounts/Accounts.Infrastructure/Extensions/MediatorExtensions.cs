namespace Accounts.Infrastructure.Extensions;

using Data;
using MediatR;
using SharedKernel.Domain;

public static class MediatorExtensions
{
    public static async Task DispatchDomainEvents(this IMediator mediator, ApplicationDbContext context)
    {
        var domainEntities = context.ChangeTracker
            .Entries<AggregateRootBase>()
            .Where(x => x.Entity.DomainEvents.Any())
            .ToList();

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        var tasks = domainEvents
            .Select(async (domainEvent) =>
            {
                await mediator.Publish(domainEvent);
            });

        await Task.WhenAll(tasks);      
        
        domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());
    }
}