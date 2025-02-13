namespace Accounts.Application.Models;

using MediatR;

public record CreateAccountCommand(Guid AggregateId, Guid CustomerId, string AccountType, string Description, decimal InitialCredit, string UserEmail): IRequest<Guid>;
