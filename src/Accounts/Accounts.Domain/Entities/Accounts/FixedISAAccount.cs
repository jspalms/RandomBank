namespace Accounts.Domain.Entities.Accounts;

using System.Diagnostics.CodeAnalysis;
using Enums;

public class FixedISAAccount: Account
{
    [SetsRequiredMembers]
    public FixedISAAccount(
        string? description,
        decimal? initialBalance,
        Guid userPortfolioID,
        Guid productOptionId) : base(AccountType.FixedSavingsAccount, description, initialBalance, userPortfolioID, productOptionId)
    {
    }
}