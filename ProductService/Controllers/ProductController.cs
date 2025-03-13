using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductService.Data.Services;
using ProductService.Models;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            var products = await _productService.GetAllProducts();
            return Ok(products);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product != null)
            {
                return Ok(product);
            }
            else 
            {
                return NotFound(null);
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult<string>> CreateNewProduct(Product newProduct)
        {
            await _productService.AddNewProduct(newProduct);
            return Created("", "Product created successfully");
            //Created() requires a URI as the first argument, which can be an empty string ("") if you don't have a specific location to return.

        }
    }
}
