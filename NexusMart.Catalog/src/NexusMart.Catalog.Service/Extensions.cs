using NexusMart.Catalog.Service.Dtos;
using NexusMart.Catalog.Service.Entities;

namespace NexusMart.Catalog.Service
{
    public static class Extensions
    {
        public static ProductDto AsDto(this Product product)
        {
            return new ProductDto(product.Id, product.Description, product.Brand, product.Barcode, product.Price, product.CreatedDate);
        }
    }
}