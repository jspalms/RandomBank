namespace Accounts.Domain.Entities;

public class ProductOption
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ProductId { get; set; } 
    public required string Name { get; set; }
    public required Product Product { get; set; }
    public decimal interestRate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeactivated { get; set; }
    
}