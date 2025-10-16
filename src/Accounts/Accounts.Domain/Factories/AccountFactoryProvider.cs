namespace Accounts.Domain.Factories;

using Enums;
using Interfaces;

public static class AccountFactoryProvider
{
    public static IAccountFactory GetFactory(BusinessLine type)
    {
        return type switch
        {
            BusinessLine.Savings => new SavingsAccountFactory(),
            BusinessLine.Lending => new LendingAccountFactory(),
            _ => throw new NotSupportedException("No factory for this product type.")
        };
    }
    
}