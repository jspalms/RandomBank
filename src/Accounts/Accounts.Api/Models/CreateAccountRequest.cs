using FluentValidation;

namespace Accounts.Api.Models;

using Domain.Entities;
using Domain.Enums;

public record CreateAccountRequest(Guid CustomerId, string AccountType, string description, decimal InitialCredit);

public class CreateAccountRequestValidator : AbstractValidator<CreateAccountRequest>
{
    public CreateAccountRequestValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty();
        RuleFor(x => x.AccountType).IsEnumName(typeof(AccountType), caseSensitive: false);
        RuleFor(x => x.description).NotEmpty();
        RuleFor(x => x.InitialCredit).NotEmpty().GreaterThan(0);
    }
}

