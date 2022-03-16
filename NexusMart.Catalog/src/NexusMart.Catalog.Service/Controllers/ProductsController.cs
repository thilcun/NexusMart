using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NexusMart.Catalog.Service.Dtos;

namespace NexusMart.Catalog.Service.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<ProductDto> Get()
        {
           
        }

        [HttpGet("{id}")]
        public ActionResult<ProductDto> GetById(Guid id)
        {
            var product = null;
            
            if(product == null)
                return NotFound();
        }
        [HttpPost]
        public ActionResult<ProductDto> Post(CreateProductDto createdProductDto)
        {

        }
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, UpdateProductDto updatedProductDto)
        {
            var existingProduct = null;

            if(existingProduct == null)
                return NotFound();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var index = -1;

            if(index < 0)
                return NotFound();
                
            return NoContent();
        }
    }
}