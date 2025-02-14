namespace Accounts.Application.Models;

using MediatR;

public record OpenAccountCommand(Guid CustomerId, Guid productOptionId, string Description, decimal? InitialBalance): IRequest<Guid>;
