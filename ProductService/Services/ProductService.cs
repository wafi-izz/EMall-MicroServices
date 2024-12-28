
using Azure;
using System.Text.Json;

namespace ProductService.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _HttpClient;
        public ProductService(HttpClient HttpClient)
        {
            _HttpClient = HttpClient;
        }
        public async Task<int?> GetProductStockAsync(Int32 ProductID)
        {
            HttpResponseMessage Response = await _HttpClient.GetAsync($"products/{ProductID}");
            if (!Response.IsSuccessStatusCode)
            {
                return null;
            }

            ProductDto? Product = JsonSerializer.Deserialize<ProductDto>(
                await Response.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                });
            return Product!.Stock;
        }
        private class ProductDto
        {
            public Int32 ID { get; set; }
            public Int32 Stock {  get; set; }
        }
    }
}
