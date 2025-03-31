using ProductService.Models;

namespace ProductService.Data.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(string id);
        Task AddNewProduct(Product newProduct);
        Task UpdateProduct(string id, Product product);
        Task DeleteProduct(string id);
    }
}
