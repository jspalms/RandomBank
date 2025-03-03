namespace Accounts.Domain.Entities.Accounts;

using System.Diagnostics.CodeAnalysis;
using Enums;

public class FixedISAAccount: Account
{
    [SetsRequiredMembers]
    public FixedISAAccount(
        string? description,
        decimal balance,
        Guid userPortfolioId,
        Guid productOptionId) : base(AccountType.FixedSavingsAccount, description, balance, userPortfolioId, productOptionId)
    {
    }
}