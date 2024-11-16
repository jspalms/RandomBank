namespace Accounts.Domain.Interfaces;

public interface IBaseRepository<T> where T : IAggregateRoot
{
    Task<T> GetByIdAsync<T>(Guid id) where T : class;
    Task<List<T>> ListAsync<T>() where T : class;
    Task<T> AddAsync<T>(T entity) where T : class;
    Task UpdateAsync<T>(T entity) where T : class;
    Task DeleteAsync<T>(T entity) where T : class;
}