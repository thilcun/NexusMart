using System.Threading.Tasks;
using MassTransit;
using NexusMart.Catalog.Contracts;
using NexusMart.Common;
using NexusMart.Inventory.Service.Entities;

namespace NexusMart.Inventory.Service.Consumers
{
    public class CatalogProductUpdatedConsumer : IConsumer<CatalogProductUpdated>
    {
        private readonly IRepository<CatalogProduct> repository;

        public CatalogProductUpdatedConsumer(IRepository<CatalogProduct> repository)
        {
            this.repository = repository;
        }
        public async Task Consume(ConsumeContext<CatalogProductUpdated> context)
        {
            var message = context.Message;

            var item = await repository.GetAsync(message.ProductId);
            
            if(item == null)
            {
                item = new CatalogProduct{
                    Id = message.ProductId,
                    Brand = message.Brand,
                    Description = message.Description,
                    Barcode = message.Barcode,
                    Price = message.Price
                };

                await repository.CreateAsync(item);
                
            }
            else
            {
                item.Brand = message.Brand;
                item.Description = message.Description;
                item.Barcode = message.Barcode;
                item.Price = message.Price;

                await repository.UpdateAsync(item);
            }
        }
    }
}