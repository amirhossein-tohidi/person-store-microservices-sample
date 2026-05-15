using StoreService.Api.Domain.Common;

namespace StoreService.Api.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public decimal UnitPrice { get; private set; }

    private Product() : base()
    {
    }

    public Product(string name, decimal unitPrice)
        : base()
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name cannot be empty.", nameof(name));

        if (unitPrice <= 0)
            throw new ArgumentOutOfRangeException(nameof(unitPrice),  "Unit price must be greater than zero.");

        Name = name;
        UnitPrice = unitPrice;
    }
}