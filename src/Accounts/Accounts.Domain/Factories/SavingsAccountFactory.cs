namespace Accounts.Domain.Factories;

using Entities.Accounts;
using Enums;
using Interfaces;

public class SavingsAccountFactory: IAccountFactory
{
    public Account CreateAccount(ProductType type, string? description, decimal initialBalance, Guid userPortfolioId, Guid productOptionId)
    {
        throw new NotImplementedException();
    }
}