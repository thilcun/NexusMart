using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NexusMart.Common;
using NexusMart.Inventory.Service.Clients;
using NexusMart.Inventory.Service.Dtos;
using NexusMart.Inventory.Service.Entities;

namespace NexusMart.Inventory.Service.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase  
    {
        private readonly IRepository<InventoryItem> inventoryItemsRepository;
        //private readonly CatalogClient catalogClient; //for synchronous connection
        private readonly IRepository<CatalogProduct> catalogProductsRepository; //for asynchronous, since there is no need to check the remote api

        private readonly IOptions<InventorySettings> inventorySettings;

        public ItemsController(IOptions<InventorySettings> inventorySettings, IRepository<InventoryItem> inventoryItemsRepository, IRepository<CatalogProduct> catalogItemsRepository)//CatalogClient catalogClient)
        {
            this.inventorySettings = inventorySettings;
            this.inventoryItemsRepository = inventoryItemsRepository;
            this.catalogProductsRepository = catalogItemsRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryItem>>> GetAsync(Guid storeId)
        {
            if(storeId == Guid.Empty)
            {
                return BadRequest();
            }
            
            //var catalogProducts = await catalogClient.GetCatalogProductsAsync(); //For Synchronous
            var inventoryItemEntities = await inventoryItemsRepository.GetAllAsync(item => item.StoreId == storeId);
            var productIds = inventoryItemEntities.Select(p => p.CatalogProductId);
            var catalogProductEntities = await catalogProductsRepository.GetAllAsync(p => productIds.Contains(p.Id));

            var inventoryItemDtos = inventoryItemEntities.Select(inventoryItem => {
                var product = catalogProductEntities.SingleOrDefault(catalogProduct => catalogProduct.Id == inventoryItem.CatalogProductId);
                
                return inventoryItem.AsDto(product.Description, product.Brand, product.Barcode);
            });

            return Ok(inventoryItemDtos);
        }
        [HttpPost]
        public async Task<ActionResult> PostAsync(CreatedItemDto createdItemDto)
        {
            var inventoryId = inventorySettings.Value.InventoryId;
            if(string.IsNullOrEmpty(inventoryId.ToString()))
            {
                return StatusCode(500);
            }

            var inventoryItem = await inventoryItemsRepository.GetAsync(item => item.CatalogProductId == createdItemDto.CatalogProductId);

            if(inventoryItem != null)
                return BadRequest();

            inventoryItem = new InventoryItem{
                CatalogProductId = createdItemDto.CatalogProductId,
                Quantity = 0,
                StoreId = inventoryId, 
                AcquiredDate = DateTimeOffset.UtcNow
            };
            await inventoryItemsRepository.CreateAsync(inventoryItem);
             
            return Ok();
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> PutAsync(Guid id, TransactionItemDto transactionItemDto)
        {
            var transactedItem = await inventoryItemsRepository.GetAsync(item => item.Id == id);

            if(transactedItem == null)
                return NotFound();

            //Check if Dto Quantity is greater than Item Quantity
            bool IsQuantityGreater = transactionItemDto.Quantity < transactedItem.Quantity;

            //Property Type indicates if the transaction is a purchase/add or sale/subtract
            bool IsQuantityAvailable = transactionItemDto.Type == 1 ? IsQuantityGreater : true; 

            //Returns BadRequest if tried to subtract a Larger amount from Quantity
            if(!IsQuantityAvailable)
                return BadRequest();

            transactedItem.Quantity = transactionItemDto.Type == 0 ? transactedItem.Quantity + transactionItemDto.Quantity : transactedItem.Quantity - transactionItemDto.Quantity;
            await inventoryItemsRepository.UpdateAsync(transactedItem);

            return Ok();
        }
    }
}