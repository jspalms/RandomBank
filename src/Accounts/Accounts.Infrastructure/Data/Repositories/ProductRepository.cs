namespace Accounts.Infrastructure.Data.Repositories;

using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class ProductRepository(ApplicationDbContext dbContext, IMediator mediator) : BaseRepository<Product>(dbContext, mediator), IProductRepository
{
    public Task<Product?> GetProductByProductOptionIdAsync(Guid productOptionId)
    {
        return dbContext.Products
            .Include(p => p.ProductOptions)
            .FirstOrDefaultAsync(p => p.ProductOptions.Any(po => po.Id == productOptionId));
    }
}