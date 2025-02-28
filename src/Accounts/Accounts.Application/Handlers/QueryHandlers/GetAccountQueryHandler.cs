namespace Accounts.Application.Handlers.QueryHandlers;

using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;
using Models;
using Models.Queries;

public class GetAccountQueryHandler(IPortfolioRepository portfolioRepository) : IRequestHandler<GetAccountQuery, AccountDetailsDTO?>
{
    public async Task<AccountDetailsDTO?> Handle(
        GetAccountQuery query,
        CancellationToken cancellationToken)
    {
        var portfolioAggregate = await portfolioRepository.GetByIdAsync(query.UserId, cancellationToken);
        if (portfolioAggregate == null)
        {
            throw new PortfolioNotFoundException(query.UserId);
        }
        return portfolioAggregate
            .UserAccounts
            .Where(a => a.Id == query.AccountId)
            .Select(x => new AccountDetailsDTO())
            .SingleOrDefault();
    }
}