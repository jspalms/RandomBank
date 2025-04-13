using SharedKernel.Domain.Interfaces;

namespace Users.Domain.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : IAggregateRoot
{
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<TEntity>> GetAllAsync();
    Task AddAsync(TEntity entity);
    void DeleteAsync(TEntity entity);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}  