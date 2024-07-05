using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }
        [HttpGet("Paging")]
        public async Task<IActionResult> GetProducts(int pageNumber = 1, int pageSize = 10)
        {
            var products = await _productService.GetProductsAsync(pageNumber, pageSize);
            return Ok(products);
        }


        // GET: api/products/limit/{limit}
        [HttpGet("limit/{limit}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByLimit(int limit)
        {
            var products = await _productService.GetProductsByLimitAsync(limit);
            return Ok(products);
        }

        // GET: api/products/sort/{sortOrder}
        [HttpGet("sort/{sortOrder}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsBySortOrder(string sortOrder)
        {
            var products = await _productService.GetProductsBySortOrderAsync(sortOrder);
            return Ok(products);
        }

        // GET: api/products/category/{categoryId}
        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByCategory(int categoryId)
        {
            var products = await _productService.GetProductsByCategoryAsync(categoryId);
            return Ok(products);
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // POST: api/products
        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct(ProductDto productDto)
        {
            if (productDto == null)
            {
                return BadRequest();
            }

            var createdProduct = await _productService.CreateProductAsync(productDto);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
        }

        // PUT: api/products/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductDto productDto)
        {
            if (id != productDto.Id)
            {
                return BadRequest();
            }

            var updatedProduct = await _productService.UpdateProductAsync(id, productDto);
            if (updatedProduct == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/products/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProductAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
