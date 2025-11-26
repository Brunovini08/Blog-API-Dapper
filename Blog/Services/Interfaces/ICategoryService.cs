using Blog.API.Models;
using Blog.API.Models.DTOs.Category;

namespace Blog.API.Services.Interfaces
{
    public interface ICategoryService
    {
        public Task<List<CategoryResponseDTO>> GetAllCategoriesAsync();
        public Task<Category> GetByIdCategoryAsync(int id);
        public Task CreateCategoryAsync(CategoryRequestDTO category);
        public Task UpdateCategoryAsync(CategoryRequestDTO category, int id);
        public Task DeleteCategoryAsync(int id);
    }
}
