namespace E_commerce.Services
{
    public class CategoryService:ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoryService(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            await _categoryRepository.CreateAsync(category);
            return category;
        }

        public async Task<Category> UpdateCategoryAsync(int id, Category category)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(id);
            if (existingCategory == null)
            {
                return null;
            }

            existingCategory.Name = category.Name;
            await _categoryRepository.UpdateAsync(existingCategory);
            return existingCategory;
        }

        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            return await _categoryRepository.DeleteAsync(categoryId);
        }
    }
}
