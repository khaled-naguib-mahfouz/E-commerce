using E_commerce.DTOs;

namespace E_commerce.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<IEnumerable<ProductDto>> GetProductsByLimitAsync(int limit);
        Task<IEnumerable<ProductDto>> GetProductsBySortOrderAsync(string sortOrder);
        Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId);
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<ProductDto> CreateProductAsync(ProductDto productDto);
        Task<ProductDto> UpdateProductAsync(int id, ProductDto productDto);
        Task<bool> DeleteProductAsync(int id);
        Task<List<Product>> GetProductsAsync(int pageNumber, int pageSize);

    }
}
