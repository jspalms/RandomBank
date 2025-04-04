using MediatR;
using Users.Domain.Entities;
using Users.Domain.ValueObjects;

namespace Users.Application.Models;

public record CreateUserCommand(Guid userId, string FirstName, string LastName, Email UserEmail) : IRequest<User>;