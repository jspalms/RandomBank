namespace Users.Api.Models;

using FluentValidation;

public record CreateUserRequest(Guid UserId, string UserEmail, string FirstName, string LastName);

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.UserEmail).EmailAddress();
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty(); 
    }
}