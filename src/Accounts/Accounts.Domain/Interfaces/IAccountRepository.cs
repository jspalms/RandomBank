using Accounts.Domain.Entities;

namespace Accounts.Domain.Interfaces;
internal interface IAccountRepository
{
    Task<Account> GetAsync(int id);
    Task<Account> GetByCustomerIdAsync(Guid customerId);
    Task AddAsync(Account account);
    Task UpdateAsync(Account account);
    Task DeleteAsync(Account account);
}
