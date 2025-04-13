using MediatR;
using Users.Domain.ValueObjects;

namespace Users.Application.Models.Commands;

public record UpdateUserCommand(Guid UserId, string FirstName, string LastName, Email Email) : IRequest;