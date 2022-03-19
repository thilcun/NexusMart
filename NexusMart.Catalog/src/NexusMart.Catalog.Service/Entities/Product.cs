using System;
using NexusMart.Common;

namespace NexusMart.Catalog.Service.Entities
{

    public class Product : IEntity
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}