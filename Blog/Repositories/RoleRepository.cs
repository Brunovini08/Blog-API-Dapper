using Blog.API.Data;
using Blog.API.Models;
using Blog.API.Models.DTOs.Category;
using Blog.API.Models.DTOs.Role;
using Blog.API.Repositories.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Blog.API.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly SqlConnection _connection;
        public RoleRepository(ConnectionDB connectionDB)
        {
            _connection = connectionDB.GetConnection();
        }
        public async Task CreateRoleAsync(Role role)
        {
            try
            {
                var sql = "INSERT INTO Role (Name, Slug) VALUES (@Name, @Slug)";
                await _connection.ExecuteAsync(sql, new { role.Name, role.Slug });
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteRoleAsync(int id)
        {
            try
            {
                var sql = "DELETE Role WHERE Id = @Id";
                await _connection.ExecuteAsync(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<RoleResponseDTO>> GetAllRolesAsync()
        {
            try
            {
                var sql = "SELECT Name, Slug FROM Role";
                var roles = (await _connection.QueryAsync<RoleResponseDTO>(sql)).ToList();
                return roles;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Role> GetRoleByIdAsync(int id)
        {
            try
            {
                var sql = "SELECT * FROM Role WHERE Id = @Id";
                var role = await _connection.QueryFirstOrDefaultAsync<Role>(sql, new { id });
                return role;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateRoleAsync(Role role, int id)
        {
            try
            {
                var sql = "UPDATE Role SET Name = @Name, Slug = @Slug WHERE Id = @Id";
                await _connection.ExecuteAsync(sql, new { role.Name, role.Slug, id});
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("Violation of UNIQUE KEY"))
                    throw new Exception("Já existe uma role com esse nome");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
