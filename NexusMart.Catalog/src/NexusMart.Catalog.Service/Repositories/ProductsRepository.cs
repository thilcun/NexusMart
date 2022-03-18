using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using NexusMart.Catalog.Service.Entities;

namespace NexusMart.Catalog.Service.Repositories
{
    public class ProductsRepository
    {
        private const string collectionName = "products";

        private readonly IMongoCollection<Product> dbCollection;
        private readonly FilterDefinitionBuilder<Product> filterBuilder = Builders<Product>.Filter;

        public ProductsRepository()
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");
            var database = mongoClient.GetDatabase("Catalog");
            dbCollection = database.GetCollection<Product>(collectionName);
        }

        public async Task<IReadOnlyCollection<Product>> GetAllAsync()
        {
            return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
        }

        public async Task<Product> GetAsync(Guid id){
            FilterDefinition<Product> filter = filterBuilder.Eq(entity => entity.Id, id);
            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Product entity)
        {
            if(entity == null)
                throw new ArgumentNullException(nameof(entity));

            await dbCollection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(Product entity)
        {
            if(entity == null)
                throw new ArgumentNullException(nameof(entity));

            FilterDefinition<Product> filter = filterBuilder.Eq(existingEntity => existingEntity.Id, entity.Id);
            await dbCollection.ReplaceOneAsync(filter, entity);
        }
        public async Task RemoveAsync(Guid id)
        {
            FilterDefinition<Product> filter = filterBuilder.Eq(entity => entity.Id, id);
            await dbCollection.DeleteOneAsync(filter);
        }
    }
}