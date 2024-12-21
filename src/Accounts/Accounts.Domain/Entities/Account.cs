namespace Accounts.Domain.Entities;

using Accounts.Domain.DomainEvents;
using Accounts.Domain.Exceptions;
using System.Diagnostics.CodeAnalysis;
using SharedKernel.Domain;

public class Account: AggregateRoot
{
    public required AccountType AccountType { get; set; }
    public required string Description { get; set; }
    public decimal Balance { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public Guid CustomerId { get; private set; }
    public bool IsClosed { get; private set; }
    public List<Transaction> Transactions { get; private set; } = new List<Transaction>();
    
    [SetsRequiredMembers]
    public Account(Guid customerId, AccountType accountType, string description, decimal initialCredit)
    {
        Id = Guid.NewGuid();
        Version = 0;
        CustomerId = customerId;
        AccountType = accountType;
        Description = description;
        Balance = initialCredit;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        AddDomainEvent(new AccountOpenedEvent(Id));
    }

    public void Credit(decimal amount, string reference)
    {
        Balance += amount;
        Transactions.Add(new Transaction
        {
            Type = TransactionType.Credit,
            CreatedAt = DateTime.UtcNow,
            Reference = reference,
            Amount = amount
        });
    }

    public void Debit(decimal amount, string reference)
    {
        if (Balance < amount)
        {
            throw new InsufficientFundsException();
        }
        Balance -= amount;
        Transactions.Add(new Transaction
        {
            Type = TransactionType.Debit,
            CreatedAt = DateTime.UtcNow,
            Reference = reference,
            Amount = amount
        });
    }
    public void Update(string description)
    {
        Description = description;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Close()
    {
        IsClosed = true;
        UpdatedAt = DateTime.UtcNow;
        AddDomainEvent(new AccountClosedEvent(Id));
    }
}
