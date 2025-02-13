namespace Accounts.Domain.Entities.Accounts;

using System.Diagnostics.CodeAnalysis;
using Enums;

public class VariableISAAccount: Account
{
    [SetsRequiredMembers]
    public VariableISAAccount(
        string description,
        decimal balance,
        Guid userPortfolioID) : base(AccountType.VariableSavingsAccount, description, balance, userPortfolioID)
    {
    }
    
}