using FluentValidation;

namespace Accounts.Api.Models;

using Domain.Enums;

public record OpenAccountRequest(Guid productOptionId, string description, decimal? initialCredit);

public class CreateAccountRequestValidator : AbstractValidator<OpenAccountRequest>
{
    public CreateAccountRequestValidator()
    {
        
        RuleFor(x => x.description).NotEmpty();
    }
}

