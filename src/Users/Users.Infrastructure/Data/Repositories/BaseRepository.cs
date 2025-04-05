using Microsoft.EntityFrameworkCore;
using SharedKernel.Domain.Interfaces;
using Users.Domain.Interfaces;
using Users.Infrastructure.Events.DomainEvents;

namespace Users.Infrastructure.Data.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IAggregateRoot
{
    private readonly ApplicationDbContext _dbContext;
    
    protected BaseRepository(
        ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        return _dbContext.Set<TEntity>().AnyAsync(e => e.Id == id);
    }

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<TEntity>().FindAsync([id], cancellationToken);
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await _dbContext.Set<TEntity>().ToListAsync();
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dbContext.Set<TEntity>().AddAsync(entity);
    }

    public void DeleteAsync(TEntity entity)
    {
        _dbContext.Set<TEntity>().Remove(entity);
    }

    public async Task SaveChangesAsync()
    {
        //increment version of all aggregate roots for concurrency checking
        foreach (var entry in _dbContext.ChangeTracker.Entries<IAggregateRoot>())
        {
            if (entry.State == EntityState.Modified)
            {
                entry.Entity.IncrementVersion();
            }
        }

        //Get all domain events and save to outbox

        var domainEntities = _dbContext.ChangeTracker
            .Entries<IAggregateRoot>()
            .Where(x => x.Entity.DomainEvents.Any())
            .ToList();

        //Publish domain events to outbox

        _dbContext.Set<OutboxMessage>().AddRange(domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .Select(domainEvent => new OutboxMessage(domainEvent)));

        await _dbContext.SaveChangesAsync();

        //dispatching domain events after saving changes - ensures that the events are not dispatched if the database operation fails
        //trade off - means that the changes to other aggregates are not transactional - need to deal with eventual consistency
        //await _mediator.DispatchDomainEvents(_dbContext);
    }
}