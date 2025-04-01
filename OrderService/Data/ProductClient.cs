using OrderService.Model;

namespace OrderService.Data
{
    public class ProductClient
    {
        private readonly HttpClient _httpClient;

        public ProductClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ProductDTO> GetProductByID(string productId)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5000/api/Product/{productId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ProductDTO>();
        }
    }
}
