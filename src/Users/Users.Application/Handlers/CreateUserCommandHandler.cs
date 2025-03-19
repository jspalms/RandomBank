using Users.Application.Models;
using Users.Domain.Entities;

namespace Users.Application.Handlers;

public class CreateUserCommandHandler
{
    private readonly IUserRepository _userRepository;
    
    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User(request.UserEmail, request.FirstName, request.LastName);
        user.SetPassword(request.Password, _passwordHasher);
        await _userRepository.AddAsync(user);
        return Unit.Value;
    }
}

