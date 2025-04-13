using MediatR;
using Users.Application.Models.Queries;
using Users.Domain.Entities;
using Users.Domain.Interfaces;

namespace Users.Application.Handlers.QueryHandlers;

public class GetUserQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserQuery, User?>
{
    public async Task<User?> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        return await userRepository.GetByIdAsync(request.UserId, cancellationToken);
        
    }
}