using StoreService.Api.Domain.Common;
using StoreService.Api.Domain.ValueObjects;

namespace StoreService.Api.Domain.Entities;

public class Invoice : BaseEntity
{
    public CustomerInfo CustomerSnapshot { get; private set; }
    public CreationToken CreationToken { get; private set; }

    private readonly List<InvoiceItem> _items = [];

    public IReadOnlyList<InvoiceItem> Items => _items.AsReadOnly();

    public decimal TotalAmount => _items.Sum(i => i.TotalPrice);

    private Invoice(): base()
    {
    }

    public Invoice(CustomerInfo customerSnapshot, CreationToken creationToken)
    {
        CustomerSnapshot = customerSnapshot ?? throw new ArgumentNullException(nameof(customerSnapshot));
        CreationToken = creationToken ?? throw new ArgumentNullException(nameof(creationToken));
    }

    public void AddItem(InvoiceItem item)
    {
        ArgumentNullException.ThrowIfNull(item);

        _items.Add(item);
    }
    
    public bool HasItems => _items.Count > 0;
}