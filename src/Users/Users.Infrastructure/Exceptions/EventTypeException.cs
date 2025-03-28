namespace Users.Infrastructure.Exceptions;

internal class EventTypeException : Exception
{
    public EventTypeException(string message) : base(message)
    {
    }
    public EventTypeException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
