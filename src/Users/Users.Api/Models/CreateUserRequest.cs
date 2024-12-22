namespace Users.Api.Models;

using FluentValidation;

public record CreateUserRequest(string UserEmail, string FirstName, string LastName, int PhoneNumber);

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(x => x.UserEmail).EmailAddress();
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty(); 
    }
}