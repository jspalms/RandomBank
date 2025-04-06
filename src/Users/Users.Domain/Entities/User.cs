using SharedKernel.Domain;
using Users.Domain.Events;
using Users.Domain.Events.DomainEvents;
using Users.Domain.ValueObjects;

namespace Users.Domain.Entities;

public class User : AggregateRootBase
{
    public Email Email { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    //parameterless constructor for EF
    private User() { }
    public User(Guid id, Email email, string firstName, string lastName)
    {
        Id = id;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        AddDomainEvent(new UserCreatedEvent(Id, FirstName, LastName, Email));
    }
}