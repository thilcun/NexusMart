using System;
using NexusMart.Common;

namespace NexusMart.Inventory.Service.Entities
{
    public class InventoryItem : IEntity
    {
        public Guid Id { get; set; }

        public Guid StoreId { get; set; }
        
        public Guid CatalogProductId { get; set; }

        public int Quantity { get; set; }

        public DateTimeOffset AcquiredDate { get; set; }
    }
}