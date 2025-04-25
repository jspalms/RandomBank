using MediatR;
using Users.Application.Models.Queries;
using Users.Domain.Entities;
using Users.Domain.Interfaces;

namespace Users.Application.Handlers.QueryHandlers;

public class GetUsersQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUsersQuery, List<User>>
{
    public async Task<List<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return await userRepository.GetAllAsync();
    }
}