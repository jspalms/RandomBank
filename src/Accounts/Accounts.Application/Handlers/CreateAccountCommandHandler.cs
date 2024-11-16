namespace Accounts.Application.Handlers;

using Api.Models;
using MediatR;

public class CreateAccountCommandHandler: IRequestHandler<CreateAccountCommand, Guid>
{
    public Task<Guid> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        // Should there be a check to make sure that the aggregate doesn't already exist 
        
        // Create the account
        
        // Save the account also emit any domain events that are happening and clear the account - check the version number

        throw new NotImplementedException();

    }
}