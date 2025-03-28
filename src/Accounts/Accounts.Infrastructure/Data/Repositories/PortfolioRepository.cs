
namespace Accounts.Infrastructure.Data.Repositories;

using Domain.Entities;
using Domain.Interfaces;
using MediatR;

public class PortfolioRepository(ApplicationDbContext dbContext) : BaseRepository<UserPortfolio>(dbContext), IPortfolioRepository
{
}

