using FluentValidation;

namespace Accounts.Api.Models;

public record CreateAccountRequest(Guid CustomerId, string AccountType, decimal InitialCredit);

public class CreateAccountRequestValidator : AbstractValidator<CreateAccountRequest>
{
    public CreateAccountRequestValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty();
        RuleFor(x => x.AccountType).NotEmpty();
        RuleFor(x => x.InitialCredit).NotEmpty().GreaterThan(0);
    }
}

