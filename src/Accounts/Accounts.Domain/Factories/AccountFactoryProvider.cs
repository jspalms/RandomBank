namespace Accounts.Domain.Factories;

using Enums;
using Interfaces;

public static class AccountFactoryProvider
{
    public static IAccountFactory GetFactory(ProductType type)
    {
        return type switch
        {
            ProductType.ISA => new ISAAccountFactory(),
            ProductType.Savings => new SavingsAccountFactory(),
            ProductType.Lending => new LendingAccountFactory(),
            _ => throw new NotSupportedException("No factory for this product type.")
        };
    }
    
}