namespace Accounts.Domain.Factories;

using System.Diagnostics;
using Entities;
using Entities.Accounts;
using Enums;
using Interfaces;

public class ISAAccountFactory: IAccountFactory
{
    public Account CreateAccount(ProductSubType subType, string? description, decimal? initialBalance, Guid UserPortfolioID, Guid productOptionId)
    {
        return subType switch
        {
            ProductSubType.Fixed => new FixedISAAccount(description, initialBalance, UserPortfolioID, productOptionId),
            ProductSubType.Variable => new VariableISAAccount(description, initialBalance, UserPortfolioID, productOptionId),

            _ => throw new NotImplementedException($"Unsupported subtype: {subType} of product type: {ProductType.ISA}")
        };
    }
}