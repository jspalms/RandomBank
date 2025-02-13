namespace Accounts.Domain.Factories;

using System.Diagnostics;
using Entities;
using Entities.Accounts;
using Enums;
using Interfaces;

public class ISAAccountFactory: IAccountFactory
{
    public Account CreateAccount(ProductOption productOption, string? description, decimal initialBalance, string customerEmail)
    {
        return productOption.Product.SubType switch
        {
            ProductSubType.Fixed => new FixedISAAccount(productOption, description, initialBalance, customerEmail),
            ProductSubType.Variable => new VariableISAAccount(productOption, description, initialBalance, customerEmail),

            _ => throw new NotImplementedException($"Unsupported subtype: {productOption.Product.SubType} of product type: {productOption.Product.Type}")
        };
    }
}