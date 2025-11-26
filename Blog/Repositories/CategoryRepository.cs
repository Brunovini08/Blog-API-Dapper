using Blog.API.Data;
using Blog.API.Models;
using Blog.API.Models.DTOs.Category;
using Blog.API.Repositories.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Blog.API.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly SqlConnection _connection;

        public CategoryRepository(ConnectionDB connectionDB)
        {
            _connection = connectionDB.GetConnection();
        }
        public async Task<List<CategoryResponseDTO>> GetAllCategoriesAsync()
        {
            try
            {
                var sql = "SELECT Name, Slug FROM Category";
                var categories = (await _connection.QueryAsync<CategoryResponseDTO>(sql)).ToList();
                return categories;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task CreateCategoryAsync(Category category)
        {
            try
            {
                var sql = "INSERT INTO Category (Name, Slug) VALUES (@Name, @Slug)";
                await _connection.ExecuteAsync(sql, new { category.Name, category.Slug });
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            try
            {
                var sql = "SELECT * FROM Category WHERE Id = @Id";
                var category = await _connection.QueryFirstOrDefaultAsync<Category>(sql, new { id });
                return category;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateCategoryAsync(Category category, int Id)
        {

            try
            {
                var sql = "UPDATE Category SET Name = @Name, Slug = @Slug WHERE Id = @Id";
                await _connection.ExecuteAsync(sql, new { category.Name, category.Slug, Id });
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("Violation of UNIQUE KEY"))
                    throw new Exception("Já existe uma categoria com esse nome");
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
                var sql = "DELETE Category WHERE Id = @Id";
                await _connection.ExecuteAsync(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
