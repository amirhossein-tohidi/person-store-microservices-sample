using StoreService.Api.Domain.Common;

namespace StoreService.Api.Domain.Entities
{
    public class InvoiceItem : BaseEntity
    {
        public long ProductId { get; private set; }

        public string ProductNameSnapshot { get; private set; } = string.Empty;
        public decimal UnitPriceSnapshot { get; private set; }

        public int Quantity { get; private set; }

        public decimal TotalPrice => UnitPriceSnapshot * Quantity;

        private InvoiceItem() : base()
        {
        }

        public InvoiceItem(long productId, string productNameSnapshot, decimal unitPriceSnapshot, int quantity)
        {
            if (string.IsNullOrWhiteSpace(productNameSnapshot))
                throw new ArgumentException("Product name cannot be empty.", nameof(productNameSnapshot));

            if (unitPriceSnapshot <= 0)
                throw new ArgumentException("Unit price must be greater than zero.", nameof(unitPriceSnapshot));

            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));

            ProductId = productId;
            ProductNameSnapshot = productNameSnapshot;
            UnitPriceSnapshot = unitPriceSnapshot;
            Quantity = quantity;
        }
    }
}