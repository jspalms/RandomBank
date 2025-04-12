using MediatR;
using Users.Application.Models;
using Users.Application.Models.Commands;
using Users.Domain.Entities;
using Users.Domain.Interfaces;

namespace Users.Application.Handlers.CommandHandlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
{
    private readonly IUserRepository _userRepository;
    
    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User(request.UserId, request.UserEmail, request.FirstName, request.LastName);
        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();
        return user;
    }
}

