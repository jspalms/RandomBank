namespace Accounts.Application.Handlers.CommandHandlers;

using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;
using Models;
using Models.Commands;

public class OpenAccountCommandHandler(
    IPortfolioRepository portfolioRepository,
    IProductRepository productRepository
) : IRequestHandler<OpenAccountCommand, Guid>
{
    public async Task<Guid> Handle(OpenAccountCommand command, CancellationToken cancellationToken)
    {
        var portfolioAggregate = await portfolioRepository.GetByIdAsync(command.CustomerId, cancellationToken);
        
        if (portfolioAggregate == null)
        {
            throw new PortfolioNotFoundException(command.CustomerId);
        }

        var productAggregate = await productRepository.GetProductByProductOptionIdAsync(command.productOptionId);
        
        if (productAggregate == null)
        {
            throw new ProductNotFoundException(command.productOptionId);
        }
        
        var newAccountId = portfolioAggregate.OpenAccount(
            productAggregate.BusinessLine, 
            productAggregate.ProductType, 
            command.productOptionId,
            command.Description, 
            command.InitialBalance ?? 0);

        await portfolioRepository.SaveChangesAsync();

        return newAccountId;
    }
}