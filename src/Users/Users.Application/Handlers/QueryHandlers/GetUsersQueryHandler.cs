using MediatR;
using Users.Application.Models.Queries;
using Users.Domain.Entities;
using Users.Domain.Interfaces;

namespace Users.Application.Handlers.QueryHandlers;

public class GetUsersQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUsersQuery, IEnumerable<User>>
{
    public async Task<IEnumerable<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return await userRepository.GetAllAsync();
    }
}