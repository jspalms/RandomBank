namespace Accounts.Domain.Entities;

using Accounts;
using DomainEvents;
using Enums;
using Exceptions;
using Factories;
using SharedKernel.Domain;

public class UserPortfolio: AggregateRootBase
{
    public IList<Account> UserAccounts { get; private set; } = [];
    public UserISALimits UserISALimits { get; private set; }
    public UserDetails UserDetails { get; private set; }
    
    public UserPortfolio(UserDetails userDetails)
    {
        Id = Guid.NewGuid();
        UserDetails = userDetails;
        UserISALimits = new UserISALimits();
    }
    
    public Guid OpenAccount(ProductType productType, ProductSubType productSubType, Guid productOptionId, string description, decimal? initialBalance)
    {
        var accountFactory = AccountFactoryProvider.GetFactory(productType);
        var account = accountFactory.CreateAccount(productSubType, description, initialBalance, Id, productOptionId);
        UserAccounts.Add(account);
        AddDomainEvent(new AccountOpenedEvent(account.Id, Id));
        return account.Id;
    }
    
    public void CloseAccount(Guid accountId)
    {
        var account = UserAccounts.FirstOrDefault(a => a.Id == accountId);
        if (account == null)
        {
            throw new AccountNotFoundException(accountId, Id);
        }
        account.Close();
        AddDomainEvent(new AccountClosedEvent(account.Id, Id));
    }
}