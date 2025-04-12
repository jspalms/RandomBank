namespace Users.Api.Models;

public record UpdateUserRequest(Guid Id, string FirstName, string LastName, string Email);