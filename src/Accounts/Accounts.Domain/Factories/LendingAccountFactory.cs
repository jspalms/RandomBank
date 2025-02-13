namespace Accounts.Domain.Factories;

using Entities;
using Entities.Accounts;
using Interfaces;

public class LendingAccountFactory: IAccountFactory
{
    public Account CreateAccount(ProductOption productOption, string? description, decimal initialBalance, string customerEmail)
    {
        throw new NotImplementedException();
    }
}