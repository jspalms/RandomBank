namespace Accounts.Application.Models.Queries;

using MediatR;

public record GetAccountQuery(Guid AccountId, Guid UserId) : IRequest<AccountDetailsDTO?>;