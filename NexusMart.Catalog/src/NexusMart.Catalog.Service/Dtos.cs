using System;
using System.ComponentModel.DataAnnotations;

namespace NexusMart.Catalog.Service.Dtos
{
    public record ProductDto(Guid Id, string Description, string Brand, string Barcode, decimal Price, DateTimeOffset CreatedDate);

    public record CreateProductDto([Required]string Description, string Brand, [Range(0, 10000)]decimal Price, [Required]string Barcode);

    public record UpdateProductDto([Required]string Description, string Brand, [Range(0, 10000)]decimal Price, [Required]string Barcode);

}