namespace Accounts.Domain.Entities;

using Enums;
using SharedKernel.Domain;

public class Product: AggregateRootBase
{
    public required string Name { get; set; }
    public ProductType Type { get; set; }
    public ProductSubType SubType { get; set; }
    public List<ProductOption> ProductOptions { get; set; } = [];
    
    public void AddProductOption(ProductOption productOption)
    {
        // Add product option
    }
    
    public void DeactivateProductOption(Guid productOptionId)
    {
        // Deactivate product option
    }
    

}