using Blog.API.Models;
using Blog.API.Models.DTOs.Category;

namespace Blog.API.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<List<CategoryResponseDTO>> GetAllCategoriesAsync();
        public Task<Category> GetCategoryByIdAsync(int id);
        public Task CreateCategoryAsync(Category category);
        public Task UpdateCategoryAsync(Category category, int id);
        public Task DeleteCategoryAsync(int id);
    }
}
