namespace Accounts.Domain.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : IAggregateRoot
{
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<List<TEntity?>> GetAllAsync();
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    void DeleteAsync(TEntity entity);
    Task SaveChangesAsync();
}  