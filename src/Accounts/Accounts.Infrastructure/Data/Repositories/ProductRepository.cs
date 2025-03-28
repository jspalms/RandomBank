namespace Accounts.Infrastructure.Data.Repositories;

using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class ProductRepository(ApplicationDbContext dbContext) : BaseRepository<Product>(dbContext), IProductRepository
{
    private readonly ApplicationDbContext _dbContext1 = dbContext;

    public Task<Product?> GetProductByProductOptionIdAsync(Guid productOptionId)
    {
        return _dbContext1.Products
            .Include(p => p.ProductOptions)
            .FirstOrDefaultAsync(p => p.ProductOptions.Any(po => po.Id == productOptionId));
    }
}