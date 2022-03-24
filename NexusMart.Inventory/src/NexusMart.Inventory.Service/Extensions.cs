using NexusMart.Inventory.Service.Dtos;
using NexusMart.Inventory.Service.Entities;

namespace NexusMart.Inventory.Service
{
    public static class Extensions
    {
        public static InventoryItemDto AsDto(this InventoryItem item, string description, string brand, string barcode)
        {
            return new InventoryItemDto(item.CatalogProductId, description, brand, barcode, item.Quantity, item.AcquiredDate);
        }
    }
}