using Blog.API.Models;
using Blog.API.Models.DTOs.Category;
using Blog.API.Repositories;
using Blog.API.Services.Interfaces;
using Microsoft.Data.SqlClient;

namespace Blog.API.Services
{
    public class CategoryService : ICategoryService
    {
        private CategoryRepository _categoryRepository { get; set; }
        public CategoryService(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<List<CategoryResponseDTO>> GetAllCategoriesAsync()
        {
            try
            {
                return await _categoryRepository.GetAllCategoriesAsync();
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task CreateCategoryAsync(CategoryRequestDTO category)
        {
            try
            {
                var newCategory = new Category(category.Name, category.Name.ToLower().Replace(" ", "-"));
                await _categoryRepository.CreateCategoryAsync(newCategory);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Category> GetByIdCategoryAsync(int id)
        {
            try
            {
                return await _categoryRepository.GetCategoryByIdAsync(id);
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateCategoryAsync(CategoryRequestDTO category, int id)
        {
            try
            {
                var newCategory = new Category(category.Name, category.Name.ToLower().Replace(" ", "-"));
                await _categoryRepository.UpdateCategoryAsync(newCategory, id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteCategoryAsync(int id)
        {
            try
            {
                await _categoryRepository.DeleteCategoryAsync(id);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
