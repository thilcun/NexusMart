using System;
using NexusMart.Common;

namespace NexusMart.Inventory.Service.Entities
{
    public class CatalogProduct : IEntity
    {
        public Guid Id { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public string Barcode { get; set; }
        public decimal Price { get; set; }
    }
}