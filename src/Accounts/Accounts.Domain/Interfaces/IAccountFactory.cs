namespace Accounts.Domain.Interfaces;

using Entities;
using Entities.Accounts;
using Enums;

public interface IAccountFactory
{
    Account CreateAccount(ProductType type, string? description, decimal initialBalance, Guid UserPortfolioID, Guid productOptionId);
}