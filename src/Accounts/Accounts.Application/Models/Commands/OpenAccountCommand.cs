namespace Accounts.Application.Models.Commands;

using MediatR;

public record OpenAccountCommand(Guid CustomerId, Guid productOptionId, string Description, decimal? InitialBalance) : IRequest<Guid>;
