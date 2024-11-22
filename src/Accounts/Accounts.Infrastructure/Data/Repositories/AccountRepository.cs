using Accounts.Domain.Entities;
using Accounts.Domain.Interfaces;

namespace Accounts.Infrastructure.Data.Repositories;

using MediatR;

public class AccountRepository(ApplicationDbContext dbContext, IMediator mediator) : BaseRepository<Account>(dbContext, mediator), IAccountRepository
{
}

