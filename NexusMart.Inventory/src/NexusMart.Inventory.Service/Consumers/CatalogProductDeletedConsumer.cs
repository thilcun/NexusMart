using System.Threading.Tasks;
using MassTransit;
using NexusMart.Catalog.Contracts;
using NexusMart.Common;
using NexusMart.Inventory.Service.Entities;

namespace NexusMart.Inventory.Service.Consumers
{
    public class CatalogProductDeletedConsumer : IConsumer<CatalogProductDeleted>
    {
        private readonly IRepository<CatalogProduct> repository;

        public CatalogProductDeletedConsumer(IRepository<CatalogProduct> repository)
        {
            this.repository = repository;
        }
        public async Task Consume(ConsumeContext<CatalogProductDeleted> context)
        {
            var message = context.Message;

            var item = await repository.GetAsync(message.ProductId);

            if(item == null)
            {
                return;
            }

            await repository.RemoveAsync(message.ProductId);
        }
    }
}