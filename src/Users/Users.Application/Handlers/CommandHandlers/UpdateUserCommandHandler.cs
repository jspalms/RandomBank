using MediatR;
using Users.Application.Models.Commands;
using Users.Domain.Exceptions;
using Users.Domain.Interfaces;

namespace Users.Application.Handlers.CommandHandlers;

public class UpdateUserCommandHandler(IUserRepository userRepository) : IRequestHandler<UpdateUserCommand>
{
    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var userAggregate = await userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (userAggregate is null)
        {
            throw new UserNotFoundException(request.UserId);
        }
        userAggregate.UpdateUser(request.FirstName, request.LastName, request.Email);
        await userRepository.SaveChangesAsync(cancellationToken);
    }
}