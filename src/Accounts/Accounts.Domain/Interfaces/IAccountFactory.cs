namespace Accounts.Domain.Interfaces;

using Entities;
using Entities.Accounts;

public interface IAccountFactory
{
    Account CreateAccount(ProductOption productOption, string? description, decimal initialBalance, string customerEmail);
    
}