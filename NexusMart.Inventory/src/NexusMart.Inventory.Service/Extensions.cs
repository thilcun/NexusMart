using NexusMart.Inventory.Service.Dtos;
using NexusMart.Inventory.Service.Entities;

namespace NexusMart.Inventory.Service
{
    public static class Extensions
    {
        public static InventoryItemDto AsDto(this InventoryItem item)
        {
            return new InventoryItemDto(item.CatalogProductId, item.Quantity, item.AcquiredDate);
        }
    }
}