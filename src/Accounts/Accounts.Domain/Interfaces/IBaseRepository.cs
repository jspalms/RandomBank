namespace Accounts.Domain.Interfaces;

using SharedKernel.Domain.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : IAggregateRoot
{
    Task<bool> ExistsAsync(Guid id);
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<List<TEntity>> GetAllAsync();
    Task AddAsync(TEntity entity);
    void DeleteAsync(TEntity entity);
    Task SaveChangesAsync();
}  