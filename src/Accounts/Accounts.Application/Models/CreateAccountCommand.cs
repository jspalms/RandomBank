using MediatR;

namespace Accounts.Api.Models;

public record CreateAccountCommand(Guid CustomerId, string AccountType, decimal InitialCredit): IRequest<Guid>;
