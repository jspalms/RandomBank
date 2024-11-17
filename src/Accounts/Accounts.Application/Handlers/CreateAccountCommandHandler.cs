namespace Accounts.Application.Handlers;

using Api.Models;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

public class CreateAccountCommandHandler: IRequestHandler<CreateAccountCommand, Guid>
{
    public async Task<Guid> Handle(CreateAccountCommand command, CancellationToken cancellationToken, IAccountRepository accountRepository)
    {
        // Should there be a check to make sure that the aggregate doesn't already exist 
        
        // Create the account
        var account = new Account(command.CustomerId, Enum.Parse<AccountType>(command.AccountType), command.Description, command.InitialCredit);
        await accountRepository.AddAsync(account);
        await accountRepository.SaveChangesAsync();
        
        // Save the account also emit any domain events that are happening and clear the account - check the version number

        return account.Id;

    }
}