using MediatR;
using Users.Domain.Entities;
using Users.Domain.ValueObjects;

namespace Users.Application.Models.Commands;

public record CreateUserCommand(Guid UserId, string FirstName, string LastName, Email UserEmail) : IRequest<User>;