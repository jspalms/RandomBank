
namespace Accounts.Infrastructure.Data.Repositories;

using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Account = Domain.Entities.Accounts.Account;

public class PortfolioRepository(ApplicationDbContext dbContext, IMediator mediator) : BaseRepository<UserPortfolio>(dbContext, mediator), IPortfolioRepository
{
}

