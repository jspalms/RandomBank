namespace Accounts.Domain.Entities.Accounts;

using System.Diagnostics.CodeAnalysis;
using Enums;

public class VariableISAAccount: Account
{
    [SetsRequiredMembers]
    public VariableISAAccount(
        string? description,
        decimal? initialBalance,
        Guid userPortfolioID,
        Guid productOptionId) : base(AccountType.FixedSavingsAccount, description, initialBalance, userPortfolioID, productOptionId)
    {
    }
    
}