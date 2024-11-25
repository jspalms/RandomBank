using Accounts.Domain.Entities;
using Accounts.Domain.Interfaces;

namespace Accounts.Infrastructure.Data.Repositories;

using MassTransit;
using MediatR;

public class AccountRepository(ApplicationDbContext dbContext, IMediator mediator, IPublishEndpoint publisherEndpoint) : BaseRepository<Account>(dbContext, mediator, publisherEndpoint), IAccountRepository
{
}

