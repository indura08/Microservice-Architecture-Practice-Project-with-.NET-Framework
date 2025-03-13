using Microsoft.EntityFrameworkCore;
using ProductService.Models;

namespace ProductService.Data.Services
{
    public class ProductServiceClass : IProductService
    {
        private readonly AppDBContext _dbContext;

        public ProductServiceClass(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddNewProduct(Product newProduct)
        {
            _dbContext.Products.Add(newProduct);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProduct(int id)
        {
            var product = GetProductById(id);
            if (product != null)
            {
                await _dbContext.Products.Where(product => product.Id == id).ExecuteDeleteAsync();
            }
        }

        public async Task<List<Product>> GetAllProducts()
        {
            var products = await _dbContext.Products.ToListAsync();
            return products;
        }

        public async Task<Product> GetProductById(int id)
        {
            var product = await _dbContext.Products.FindAsync(id);
            if (product != null)
            {
                return product;
            }
            else
            {
                return null!;
            }
        }

        public async Task UpdateProduct(int id, Product product)
        {
            var currentProduct = await GetProductById(id);
            if (currentProduct != null)
            {
                currentProduct.ProductName = product.ProductName;
                currentProduct.Price = product.Price;
                currentProduct.IsAvailable = product.IsAvailable;

                _dbContext.Entry(currentProduct).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
