namespace Accounts.Infrastructure.Data.Repositories;

using Domain.Interfaces;
using Extensions;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IAggregateRoot
{
    private readonly ApplicationDbContext _dbContext;
    
    private readonly IMediator _mediator;
    
    protected BaseRepository(
        ApplicationDbContext dbContext, 
        IMediator mediator, 
        IPublishEndpoint publishEndpoint
        )
    {
        _dbContext = dbContext;
        _mediator = mediator;
    }
    
    public Task<bool> ExistsAsync(Guid id)
    {
        return _dbContext.Set<TEntity>().AnyAsync(e => e.Id == id);
    }

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Set<TEntity>().FindAsync(id);
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

        await _dbContext.SaveChangesAsync();

        //dispatching domain events after saving changes - ensures that the events are not dispatched if the database operation fails
        //trade off - means that the changes to other aggregates are not transactional - need to deal with eventual consistency
        await _mediator.DispatchDomainEvents(_dbContext);
    }
}