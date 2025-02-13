using Accounts.Domain.Entities;

namespace Accounts.Domain.Interfaces;

using Entities.Accounts;
using SharedKernel.Domain.Interfaces;

public interface IAccountRepository : IBaseRepository<Account>
{

}
