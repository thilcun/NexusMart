using System;

namespace NexusMart.Inventory.Service.Dtos
{
    public record InventoryItemDto(Guid CatalogProductId, string Description, string Brand, string Barcode, int Quantity, DateTimeOffset Date);
    public record CreatedItemDto(Guid CatalogProductId, DateTimeOffset CreatedDate);
    public record TransactionItemDto(int Type, Guid ItemId, int Quantity, DateTimeOffset TransactionDate);

    public record CatalogProductDto(Guid Id, string Description, string Brand, string Barcode, decimal Price);
}