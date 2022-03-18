using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NexusMart.Catalog.Service.Entities;

namespace NexusMart.Catalog.Service.Repositories
{
    public interface IProductsRepository
    {
        Task CreateAsync(Product entity);
        Task<IReadOnlyCollection<Product>> GetAllAsync();
        Task<Product> GetAsync(Guid id);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(Product entity);
    }
}