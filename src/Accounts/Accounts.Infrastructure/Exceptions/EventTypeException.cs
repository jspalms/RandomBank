namespace Accounts.Infrastructure.Exceptions;

internal class EventTypeException : Exception
{
    public EventTypeException(string message) : base(message)
    {
    }
}
