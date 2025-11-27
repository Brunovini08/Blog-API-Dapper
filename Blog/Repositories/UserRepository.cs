using Azure;
using Blog.API.Data;
using Blog.API.Models;
using Blog.API.Models.DTOs.User;
using Blog.API.Repositories.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Blog.API.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly SqlConnection _connection;
        public UserRepository(ConnectionDB connectionDB)
        {
            _connection = connectionDB.GetConnection();
        }

        public async Task CreateUserAsync(User user)
        {
            try
            {
                var sql = "INSERT INTO UserBlog (Name, Email, PasswordHash, Bio, Image, Slug) VALUES (@Name, @Email, @PasswordHash, @Bio, @Image, @Slug)";
                await _connection.ExecuteAsync(sql, new { user.Name, user.Email, user.PasswordHash, user.Bio, user.Image, user.Slug});
            }
            catch (SqlException ex)
            {

                if (ex.Message.Contains("Violation of UNIQUE KEY"))
                    throw new Exception("Este email já está cadastrado.");
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteUserAsync(int id)
        {
            var sql = "DELETE FROM UserBlog WHERE Id = @Id";
            await _connection.ExecuteAsync(sql, new { id });
        }

        public async Task<List<UserResponseDTO>> GetAllUserAsync()
        {
            try
            {
                var sql = "SELECT Id, Name, Email, Bio, Image, Slug FROM UserBlog";
                var users = (await _connection.QueryAsync<UserResponseDTO>(sql)).ToList();
                return users;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserResponseDTO> GetUserByIdAsync(int id)
        {
            try
            {
                var sql = "SELECT Id, Name, Email, Bio, Image, Slug FROM Tag WHERE Id = @Id";
                var user = await _connection.QueryFirstOrDefaultAsync<UserResponseDTO>(sql, new { id });
                return user;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task UpdateUserAsync(User user, int id)
        {
            throw new NotImplementedException();
        }
    }
}
