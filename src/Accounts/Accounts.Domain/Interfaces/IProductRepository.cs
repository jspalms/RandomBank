namespace Accounts.Domain.Interfaces;

using Entities;

public interface IProductRepository : IBaseRepository<Product>
{
    public Task<Product?> GetProductByProductOptionIdAsync(Guid productOptionId);
}