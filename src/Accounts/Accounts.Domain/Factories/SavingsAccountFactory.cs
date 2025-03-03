namespace Accounts.Domain.Factories;

using Entities.Accounts;
using Enums;
using Interfaces;

public class SavingsAccountFactory: IAccountFactory
{
    public Account CreateAccount(ProductSubType subType, string? description, decimal initialBalance, Guid UserPortfolioID, Guid productOptionId)
    {
        throw new NotImplementedException();
    }
}