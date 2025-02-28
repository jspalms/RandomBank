namespace Accounts.Application.Models.Queries;

using MediatR;

public record GetAccountsQuery(Guid UserId) : IRequest<IEnumerable<AccountDetailsDTO>>;