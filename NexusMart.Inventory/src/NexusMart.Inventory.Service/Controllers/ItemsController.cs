using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NexusMart.Common;
using NexusMart.Inventory.Service.Dtos;
using NexusMart.Inventory.Service.Entities;

namespace NexusMart.Inventory.Service.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase  
    {
        private readonly IRepository<InventoryItem> itemsRepository;

        public ItemsController(IRepository<InventoryItem> itemsRepository)
        {
            this.itemsRepository = itemsRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryItem>>> GetAsync(Guid storeId)
        {
            if(storeId == Guid.Empty)
            {
                return BadRequest();
            }
            var items = (await itemsRepository.GetAllAsync(item => item.StoreId == storeId)).Select(item => item.AsDto());

            return Ok(items);
        }
        [HttpPost]
        public async Task<ActionResult> PostAsync(CreatedItemDto createdItemDto)
        {
            var inventoryItem = await itemsRepository.GetAsync(item => item.CatalogProductId == createdItemDto.CatalogProductId);

            if(inventoryItem != null)
                return BadRequest();

            inventoryItem = new InventoryItem{
                CatalogProductId = createdItemDto.CatalogProductId,
                Quantity = 0,
                StoreId = Guid.NewGuid(), //TODO
                AcquiredDate = DateTimeOffset.UtcNow
            };
            await itemsRepository.CreateAsync(inventoryItem);
             
            return Ok();
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> PutAsync(Guid id, TransactionItemDto transactionItemDto)
        {
            var transactedItem = await itemsRepository.GetAsync(item => item.Id == id);

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
            await itemsRepository.UpdateAsync(transactedItem);

            return Ok();
        }
    }
}