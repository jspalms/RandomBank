using SharedKernel.Domain;
using Users.Domain.Events;

namespace Users.Domain.Entities;

public class User : AggregateRootBase
{
    public string Email { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public User(string email, string firstName, string lastName)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        AddDomainEvent(new UserCreatedEvent(Id, this));
    }
}