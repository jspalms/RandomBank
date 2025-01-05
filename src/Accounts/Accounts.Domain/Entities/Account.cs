namespace Accounts.Domain.Entities;

using Accounts.Domain.DomainEvents;
using Accounts.Domain.Exceptions;
using System.Diagnostics.CodeAnalysis;
using Enums;
using SharedKernel.Domain;

public class Account: AggregateRootBase
{
    public required AccountType AccountType { get; set; }
    public required string Description { get; set; }
    public decimal Balance { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public Guid CustomerId { get; private set; }
    public bool IsClosed { get; private set; }
    public List<Transaction> Transactions { get; private set; } = [];
    
    
    [SetsRequiredMembers]
    public Account(Guid customerId, AccountType accountType, string description, decimal balance)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;
        AccountType = accountType;
        Description = description;
        Balance = balance;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        AddDomainEvent(new AccountOpenedEvent{AggregateId = Id});
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
        AddDomainEvent(new TransactionAddedEvent{AggregateId = Id});
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
        AddDomainEvent(new TransactionAddedEvent{AggregateId = Id});
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
        AddDomainEvent(new AccountClosedEvent{AggregateId = Id});
    }
}
