namespace Accounts.Application.Handlers.CommandHandlers;

using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Exceptions;
using MediatR;
using Models;

public class CreateAccountCommandHandler(IAccountRepository accountRepository) : IRequestHandler<CreateAccountCommand, Guid>
{
    public async Task<Guid> Handle(CreateAccountCommand command, CancellationToken cancellationToken)
    {
        //is this normally how a check is done?
        if (await accountRepository.ExistsAsync(command.AggregateId))
        {
            throw new AggregateAlreadyExistsException(command.AggregateId);
        }

        var account = new Account(command.CustomerId, Enum.Parse<AccountType>(command.AccountType), command.Description, command.InitialCredit);

        await accountRepository.AddAsync(account);

        await accountRepository.SaveChangesAsync();

        return account.Id;
    }
}