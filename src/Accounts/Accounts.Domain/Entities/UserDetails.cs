namespace Accounts.Domain.Entities;

public record UserDetails
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public required string PhoneNumber { get; init; }
    public required string Address { get; init; }
}