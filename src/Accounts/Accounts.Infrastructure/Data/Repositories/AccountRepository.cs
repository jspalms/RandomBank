
namespace Accounts.Infrastructure.Data.Repositories;

using Domain.Interfaces;
using MediatR;
using Account = Domain.Entities.Account;

public class AccountRepository(ApplicationDbContext dbContext, IMediator mediator) : BaseRepository<Account>(dbContext, mediator), IAccountRepository
{
}

