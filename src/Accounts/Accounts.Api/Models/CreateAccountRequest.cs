using FluentValidation;

namespace Accounts.Api.Models;

public record CreateAccountRequest(Guid customerId, string accountType, decimal initialCredit);

public class CreateAccountRequestValidator : AbstractValidator<CreateAccountRequest>
{
    public CreateAccountRequestValidator()
    {
        RuleFor(x => x.customerId).NotEmpty();
        RuleFor(x => x.accountType).NotEmpty();
        RuleFor(x => x.initialCredit).NotEmpty().GreaterThan(0);
    }
}

