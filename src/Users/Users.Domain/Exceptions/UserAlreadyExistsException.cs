namespace Users.Domain.Exceptions;

public class UserAlreadyExistsException : Exception
{
    public UserAlreadyExistsException(Guid userId)
        : base($"User with ID '{userId}' already exists.")
    {
    }

    public UserAlreadyExistsException(Guid userId, Exception innerException)
        : base($"User with ID '{userId}' already exists.", innerException)
    {
    }
    
}