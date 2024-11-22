namespace Accounts.Application.Exceptions;

public class AggregateAlreadyExistsException: Exception
{
    public AggregateAlreadyExistsException(Guid aggregateId): base($"Aggregate with id {aggregateId} already exists")
    {
    }
    
}