namespace Accounts.Infrastructure.Data.Repositories;

using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IAggregateRoot
{
    public BaseRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private readonly ApplicationDbContext _dbContext;


    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Set<TEntity>().FindAsync(id);
    }

    public async Task<List<TEntity?>> GetAllAsync()
    {
        return await _dbContext.Set<TEntity>().ToListAsync();
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dbContext.Set<TEntity>().AddAsync(entity);
    }

    public async Task UpdateAsync(TEntity entity)
    {
       await _dbContext.Set<TEntity>().ExecuteUpdateAsync(entity);
    }

    public void DeleteAsync(TEntity entity)
    {
        _dbContext.Set<TEntity>().Remove(entity);
    }

    public async Task SaveChangesAsync()
    {
        // need to emit the domain events here
        await _dbContext.SaveChangesAsync();
    }
}