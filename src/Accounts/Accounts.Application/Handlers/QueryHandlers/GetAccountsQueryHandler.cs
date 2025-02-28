namespace Accounts.Application.Handlers.QueryHandlers;

using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;
using Models;
using Models.Queries;

public class GetAccountsQueryHandler(IPortfolioRepository portfolioRepository)  : IRequestHandler<GetAccountsQuery, IEnumerable<AccountDetailsDTO>>
{
    public async Task<IEnumerable<AccountDetailsDTO>> Handle(GetAccountsQuery query, CancellationToken cancellationToken)
    {
        var portfolioAggregate = await portfolioRepository.GetByIdAsync(query.UserId, cancellationToken);
        if (portfolioAggregate == null)
        {
            throw new PortfolioNotFoundException(query.UserId);
        }
        return portfolioAggregate
            .UserAccounts
            .Select(x => new AccountDetailsDTO())
            .ToList();
    }
}