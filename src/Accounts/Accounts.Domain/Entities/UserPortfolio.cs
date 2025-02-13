namespace Accounts.Domain.Entities;

using Accounts;
using DomainEvents;
using Enums;
using Factories;
using SharedKernel.Domain;

public class UserPortfolio: AggregateRootBase
{
    public IList<Account> UserAccounts { get; private set; } = [];
    public UserISALimits UserISALimits { get; private set; }
    public UserDetails UserDetails { get; private set; }
    
    public UserPortfolio(UserDetails userDetails, UserISALimits userISALimits)
    {
        Id = Guid.NewGuid();
        UserDetails = userDetails;
        UserISALimits = new UserISALimits();
    }
    
    public void OpenAccount(ProductOption option, string description, decimal initialBalance)
    {
        var accountFactory = AccountFactoryProvider.GetFactory(option.Product.Type);
        var account = accountFactory.CreateAccount(option, description, initialBalance, UserDetails.Email);
        UserAccounts.Add(account);
        AddDomainEvent(new AccountOpenedEvent(account.Id, Id));
    }
    
    public void CloseAccount(Guid accountId)
    {
        var account = UserAccounts.FirstOrDefault(a => a.Id == accountId);
        if (account == null)
        {
            throw new AccountNotFoundException();
        }
        account.Close();
        AddDomainEvent(new AccountClosedEvent(account.Id, Id));
    }
}