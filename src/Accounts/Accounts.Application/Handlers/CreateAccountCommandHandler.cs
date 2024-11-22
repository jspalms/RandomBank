namespace Accounts.Application.Handlers;

using Domain.Entities;
using Domain.Interfaces;
using Exceptions;
using MediatR;
using Models;

public class CreateAccountCommandHandler(IAccountRepository accountRepository): IRequestHandler<CreateAccountCommand, Guid>
{
    public async Task<Guid> Handle(CreateAccountCommand command, CancellationToken cancellationToken)
    {
        // Should there be a check to make sure that the aggregate doesn't already exist 

        if(await accountRepository.ExistsAsync(command.AggregateId))
        {
            throw new AggregateAlreadyExistsException(command.AggregateId);
        }
        
        // Create the account
        
        var account = new Account(command.CustomerId, Enum.Parse<AccountType>(command.AccountType), command.Description, command.InitialCredit);
        await accountRepository.AddAsync(account);
        await accountRepository.SaveChangesAsync();
        
        // Save the account also emit any domain events that are happening and clear the account - check the version number

        return account.Id;

    }
    
}