using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using NexusMart.Catalog.Contracts;
using NexusMart.Catalog.Service.Dtos;
using NexusMart.Catalog.Service.Entities;
using NexusMart.Common;

namespace NexusMart.Catalog.Service.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductsController : ControllerBase
    {
        
        private readonly IRepository<Product> productsRepository;
        private readonly IPublishEndpoint publishEndpoint;
        public ProductsController(IRepository<Product> productsRepository, IPublishEndpoint publishEndpoint)
        {
            this.productsRepository = productsRepository;
            this.publishEndpoint = publishEndpoint;
        }
        [HttpGet]
        public async Task<IEnumerable<ProductDto>> GetAsync()
        {
            var products = (await productsRepository.GetAllAsync()).Select(products => products.AsDto());

            return products;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetByIdAsync(Guid id)
        {
            var product = await productsRepository.GetAsync(id);
            
            if(product == null)
                return NotFound();

            return product.AsDto();
        }
        [HttpPost]
        public async Task<ActionResult<ProductDto>> PostAsync(CreateProductDto createdProductDto)
        {
            Product product = new Product{
                Id = Guid.NewGuid(),
                Description = createdProductDto.Description,
                Brand = createdProductDto.Brand,
                Price = createdProductDto.Price,
                Barcode = createdProductDto.Barcode,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await productsRepository.CreateAsync(product);

            await publishEndpoint.Publish(new CatalogProductCreated(product.Id, product.Description, product.Brand, product.Barcode, product.Price));

            return CreatedAtAction(nameof(GetByIdAsync), new { id = product.Id }, product);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateProductDto updatedProductDto)
        {
            var existingProduct = await productsRepository.GetAsync(id);
            
            if(existingProduct == null)
                return NotFound();

            existingProduct.Description = updatedProductDto.Description;
            existingProduct.Brand = updatedProductDto.Brand;
            existingProduct.Price = updatedProductDto.Price;
            existingProduct.Barcode = updatedProductDto.Barcode;

            await productsRepository.UpdateAsync(existingProduct);

            await publishEndpoint.Publish(new CatalogProductUpdated(existingProduct.Id, existingProduct.Description, existingProduct.Brand, existingProduct.Barcode, existingProduct.Price));

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var product = await productsRepository.GetAsync(id);

            if(product == null)
                return NotFound();

            await productsRepository.RemoveAsync(product.Id);

            await publishEndpoint.Publish(new CatalogProductDeleted(id));

            return NoContent();
        }
    }
}