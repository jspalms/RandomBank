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
    public UserDetails UserDetails { get; set; }

    //required for EF
    #pragma warning disable CS8618 
    private UserPortfolio() {}
    #pragma warning restore CS8618 

    
    public UserPortfolio(UserDetails userDetails)
    {
        Id = Guid.NewGuid();
        UserDetails = userDetails;
        UserISALimits = new UserISALimits();
    }
    
    public Guid OpenAccount(BusinessLine businessLine, ProductType productType, Guid productOptionId, string description, decimal initialBalance)
    {
        var accountFactory = AccountFactoryProvider.GetFactory(businessLine);
        var account = accountFactory.CreateAccount(productType, description, initialBalance, Id, productOptionId);

        if (account is FixedISAAccount isaAccount)
        {
            if (!UserISALimits.CanOpenNewISAAccount(isaAccount))
            {
                throw new ISALimitExceededException(Id);
            }
            UserISALimits.AddISAAccount(isaAccount);
        }
        else if (account is SavingsAccount || account is CurrentAccount)
        {
            
        }
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