using System;
using System.ComponentModel.DataAnnotations;

namespace NexusMart.Catalog.Service.Dtos
{
    public record ProductDto(Guid Id, string Description, string Brand, decimal Price, DateTimeOffset CreatedDate);

    public record CreateProductDto([Required]string Description, string Brand, [Range(0, 10000)]decimal Price);

    public record UpdateProductDto([Required]string Description, string Brand, [Range(0, 10000)]decimal Price);

}