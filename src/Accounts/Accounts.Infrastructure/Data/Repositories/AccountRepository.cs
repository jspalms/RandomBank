using Accounts.Domain.Entities;
using Accounts.Domain.Interfaces;

namespace Accounts.Infrastructure.Data.Repositories;



public class AccountRepository(ApplicationDbContext dbContext) : BaseRepository<Account>(dbContext), IAccountRepository
{
}

