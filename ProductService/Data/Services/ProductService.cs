using ProductService.Models;

namespace ProductService.Data.Services
{
    public class ProductService : IProductService
    {
        public Task AddNewProduct(Product newProduct)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProduct(int id, Product product)
        {
            throw new NotImplementedException();
        }

        // todo - implement thees method for product service 
    }
}
