using System.Threading.Tasks;
using MassTransit;
using NexusMart.Catalog.Contracts;
using NexusMart.Common;
using NexusMart.Inventory.Service.Entities;

namespace NexusMart.Inventory.Service.Consumers
{
    public class CatalogProductCreatedConsumer : IConsumer<CatalogProductCreated>
    {
        private readonly IRepository<CatalogProduct> repository;

        public CatalogProductCreatedConsumer(IRepository<CatalogProduct> repository)
        {
            this.repository = repository;
        }
        public async Task Consume(ConsumeContext<CatalogProductCreated> context)
        {
            var message = context.Message;

            var item = await repository.GetAsync(message.ProductId);
            
            if(item != null)
            {
                return;
            }

            item = new CatalogProduct{
                Id = message.ProductId,
                Brand = message.Brand,
                Description = message.Description,
                Barcode = message.Barcode,
                Price = message.Price
            };

            await repository.CreateAsync(item);

        }
    }
}