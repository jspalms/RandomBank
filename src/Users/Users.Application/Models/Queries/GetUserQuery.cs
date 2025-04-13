using MediatR;
using Users.Domain.Entities;

namespace Users.Application.Models.Queries;

public record GetUserQuery(Guid UserId): IRequest<User?>;