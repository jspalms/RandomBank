namespace Accounts.Domain.Entities.Accounts;

using System.Diagnostics.CodeAnalysis;
using Enums;
using Exceptions;

public abstract class Account
{
    public Guid Id { get; private set; }
    public required AccountType AccountType { get; set; }
    public  string? Description { get; set; }
    public decimal Balance { get; set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public required Guid UserPortfolioId { get; set; }
    public UserPortfolio UserPortfolio { get; set; }
    public bool IsClosed { get; private set; }
    public Guid ProductOptionId { get; private set; }
    public List<Transaction> Transactions { get; private set; } = [];
    
    
    [SetsRequiredMembers]
    protected Account(AccountType accountType, string? description, decimal balance, Guid userPortfolioId, Guid productOptionId)
    {
        Id = Guid.NewGuid();
        AccountType = accountType;
        Description = description;
        Balance = balance;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        UserPortfolioId = userPortfolioId;
        ProductOptionId = productOptionId;
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
    }
}
