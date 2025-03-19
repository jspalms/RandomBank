namespace Users.Application.Models;

public record CreateUserCommand(string UserEmail, string FirstName, string LastName);