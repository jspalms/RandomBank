using Accounts.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Domain.Entities;
public class Account
{
    public int Id { get; private set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal Balance { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public Guid CustomerId { get; private set; }
    public bool IsDeleted { get; private set; }
    public List<Transaction> Transactions { get; private set; } = new List<Transaction>();

    public Account(Guid customerId, string name, string description, decimal initialCredit)
    {
        CustomerId = customerId;
        Name = name;
        Description = description;
        Balance = initialCredit;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;

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

    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Delete()
    {
        IsDeleted = true;
        UpdatedAt = DateTime.UtcNow;
    }
}
