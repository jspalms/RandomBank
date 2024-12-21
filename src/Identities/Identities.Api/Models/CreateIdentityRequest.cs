namespace Identities.Api.Models;

using FluentValidation;

public record CreateIdentityRequest(string UserEmail, string FirstName, string LastName, int PhoneNumber);

public class CreateIdentityRequestValidator : AbstractValidator<CreateIdentityRequest>
{
    public CreateIdentityRequestValidator()
    {
        RuleFor(x => x.UserEmail).EmailAddress();
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty(); 
    }
}