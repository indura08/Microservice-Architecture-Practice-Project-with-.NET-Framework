using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProductService.Models;

namespace ProductService.Data.Services
{
    public class ProductServiceClass : IProductService
    {
        private readonly IMongoCollection<Product> _products;
        public ProductServiceClass(IOptions<MongoDBSettings> settings, IMongoClient mongoClient)
        {
            var datatbase = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _products = datatbase.GetCollection<Product>(settings.Value.ProductCollectionName);
        }

        public async Task AddNewProduct(Product newProduct)
        {
            await _products.InsertOneAsync(newProduct);
        }

        public async Task DeleteProduct(string id)
        {
            var product = GetProductById(id);
            if (product != null)
            {
                await _products.DeleteOneAsync(p => p.Id == id);
            }
        }

        public async Task<List<Product>> GetAllProducts()
        {
            var products = await _products.Find(p => true).ToListAsync();
            return products;
        }

        public async Task<Product> GetProductById(string id)
        {
            var product = await _products.Find(p => p.Id == id).FirstOrDefaultAsync();
            if (product != null)
            {
                return product;
            }
            else
            {
                return null!;
            }
        }

        public async Task UpdateProduct(string id, Product product)
        {
            await _products.ReplaceOneAsync(p => product.Id == id, product);
        }
    }
}
