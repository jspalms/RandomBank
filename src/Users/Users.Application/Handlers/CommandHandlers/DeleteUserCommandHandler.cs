using MediatR;
using Users.Application.Models.Commands;
using Users.Domain.Exceptions;
using Users.Domain.Interfaces;

namespace Users.Application.Handlers.CommandHandlers;

public class DeleteUserCommandHandler(IUserRepository userRepository) : IRequestHandler<DeleteUserCommand>
{
    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var userAggregate = await userRepository.GetByIdAsync(request.Id, cancellationToken);
        if (userAggregate is null)
        {
           throw new UserNotFoundException(request.Id);
        }
        userAggregate.DeleteUser();
        await userRepository.SaveChangesAsync(cancellationToken);
    }
}