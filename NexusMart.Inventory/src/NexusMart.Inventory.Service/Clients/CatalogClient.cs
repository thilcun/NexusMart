using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using NexusMart.Inventory.Service.Dtos;

namespace NexusMart.Inventory.Service.Clients
{
    public class CatalogClient
    {
        private readonly HttpClient httpClient;

        public CatalogClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IReadOnlyCollection<CatalogProductDto>> GetCatalogProductsAsync()
        {
            var products = await httpClient.GetFromJsonAsync<IReadOnlyCollection<CatalogProductDto>>("/products");

            return products;
        }
    }
}