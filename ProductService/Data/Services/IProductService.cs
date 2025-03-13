using ProductService.Models;

namespace ProductService.Data.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task AddNewProduct(Product newProduct);
        Task UpdateProduct(int id, Product product);
        Task DeleteProduct(int id);
    }
}
