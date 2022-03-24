using System;

namespace NexusMart.Catalog.Contracts
{
    public record CatalogProductCreated(Guid ProductId, string Description, string Name, string Barcode, decimal Price);
    public record CatalogProductUpdated(Guid ProductId, string Description, string Name, string Barcode, decimal Price);
    public record CatalogProductDeleted(Guid ProductId);
}