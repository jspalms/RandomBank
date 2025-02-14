namespace Accounts.Domain.Exceptions;

public class ProductNotFoundException: Exception
{
    public ProductNotFoundException(Guid productOptionId) : base($"Product with product option id {productOptionId} was not found")
    {
    }
}