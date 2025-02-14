namespace Accounts.Domain.Interfaces;

using Entities;
using Entities.Accounts;
using Enums;

public interface IAccountFactory
{
    Account CreateAccount(ProductSubType subType, string? description, decimal? initialBalance, Guid UserPortfolioID, Guid productOptionId);
}