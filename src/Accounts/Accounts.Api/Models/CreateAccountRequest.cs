using FluentValidation;

namespace Accounts.Api.Models;

using Domain.Enums;

public record CreateAccountRequest(Guid productOptionId, string description, decimal? initialCredit);

public class CreateAccountRequestValidator : AbstractValidator<CreateAccountRequest>
{
    public CreateAccountRequestValidator()
    {
        
        RuleFor(x => x.description).NotEmpty();
    }
}

