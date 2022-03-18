using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NexusMart.Catalog.Service.Dtos;
using NexusMart.Catalog.Service.Entities;
using NexusMart.Catalog.Service.Repositories;

namespace NexusMart.Catalog.Service.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsRepository productsRepository = new();
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
                CreatedDate = DateTimeOffset.UtcNow
            };

            await productsRepository.CreateAsync(product);

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

            await productsRepository.UpdateAsync(existingProduct);

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var product = await productsRepository.GetAsync(id);

            if(product == null)
                return NotFound();

            await productsRepository.RemoveAsync(product.Id);

            return NoContent();
        }
    }
}