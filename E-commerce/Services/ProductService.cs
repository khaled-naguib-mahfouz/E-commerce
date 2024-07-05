namespace E_commerce.Services
{
    public class ProductService:IProductService
    {
        private readonly IRepository<Product> _productRepository;

        public ProductService(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<Product>> GetProductsAsync(int pageNumber, int pageSize)
        {
            // Calculate the number of items to skip
            int itemsToSkip = (pageNumber - 1) * pageSize;

            // Query the repository to get paginated products
            return await _productRepository
                .Query()
                .Skip(itemsToSkip)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                CategoryId = p.CategoryId,
                ImageUrl=p.ImageUrl
                
            });
        }

        public async Task<IEnumerable<ProductDto>> GetProductsByLimitAsync(int limit)
        {
            var products = await _productRepository.GetAllAsync();
            return products.Take(limit).Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                CategoryId = p.CategoryId,
                ImageUrl=p.ImageUrl
            });
        }

        public async Task<IEnumerable<ProductDto>> GetProductsBySortOrderAsync(string sortOrder)
        {
            var products = await _productRepository.GetAllAsync();

            if (sortOrder == "asc")
            {
                products = products.OrderBy(p => p.Name);
            }
            else if (sortOrder == "desc")
            {
                products = products.OrderByDescending(p => p.Name);
            }

            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                CategoryId = p.CategoryId,
                ImageUrl=p.ImageUrl
            });
        }

        public async Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId)
        {
            var products = await _productRepository.GetAllAsync();
            return products.Where(p => p.CategoryId == categoryId).Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                CategoryId = p.CategoryId,
                ImageUrl=p.ImageUrl
            });
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return null;
            }

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                CategoryId = product.CategoryId,
                ImageUrl=product.ImageUrl
            };
        }

        public async Task<ProductDto> CreateProductAsync(ProductDto productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Description = productDto.Description,
                CategoryId = productDto.CategoryId,
                ImageUrl=productDto.ImageUrl
            };

            await _productRepository.CreateAsync(product);
            productDto.Id = product.Id;

            return productDto;
        }

        public async Task<ProductDto> UpdateProductAsync(int id, ProductDto productDto)
        {
            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
            {
                return null;
            }

            existingProduct.Name = productDto.Name;
            existingProduct.Price = productDto.Price;
            existingProduct.Description = productDto.Description;
            existingProduct.CategoryId = productDto.CategoryId;
            existingProduct.ImageUrl = productDto.ImageUrl;
            await _productRepository.UpdateAsync(existingProduct);

            return productDto;
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            return await _productRepository.DeleteAsync(productId);
        }
    }
}
