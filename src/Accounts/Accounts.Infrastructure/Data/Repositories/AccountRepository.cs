using Accounts.Domain.Entities;
using Accounts.Domain.Interfaces;

namespace Accounts.Infrastructure.Data.Repositories;



public class AccountRepository : IAccountRepository
{
    public Task AddAsync(Account account)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Account account)
    {
        throw new NotImplementedException();
    }

    public Task<Account> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Account> GetByCustomerIdAsync(Guid customerId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Account account)
    {
        throw new NotImplementedException();
    }
}

