using Azure;
using Blog.API.Data;
using Blog.API.Models;
using Blog.API.Models.DTOs.Role;
using Blog.API.Models.DTOs.User;
using Blog.API.Repositories.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;

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
                var sql = "INSERT INTO UserBlog (Name, Email, PasswordHash, Bio, Image, Slug) VALUES (@Name, @Email, @PasswordHash, @Bio, @Image, @Slug); SELECT CAST(SCOPE_IDENTITY() AS INT)";
                int id = await _connection.ExecuteScalarAsync<int>(sql, new { user.Name, user.Email, user.PasswordHash, user.Bio, user.Image, user.Slug});

                var sqlUserRole = "INSERT INTO UserRole(UserId, RoleId) VALUES (@UserId, @RoleId)";
                await _connection.ExecuteAsync(sqlUserRole, new { UserId = id, RoleId = 2 });
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

        public async Task<List<GetAllUsersWithRoleDTO>> GetAllUsersWithRoleAsync()
        {
            try
            {
                var sql = @"SELECT u.Id, u.Name, u.Email, u.Bio, u.Image, u.Slug, r.Id as RoleId, r.Name, r.Slug as SlugRole 
                            FROM [UserBlog] u  
                            INNER JOIN UserRole ur 
                            ON u.Id = ur.UserId    
                            INNER JOIN [Role] r 
                            ON r.Id = ur.RoleId";
                var users = await _connection.QueryAsync<GetAllUsersWithRoleDTO, GetUsersRoleDTO, GetAllUsersWithRoleDTO>(sql, (user, role) =>
                {
                    user.Roles.Add(role);
                    return user;
                }, splitOn: "RoleId");

                var result = users.GroupBy(u => u.Id).Select(g =>
                {
                    var groupedUsers = g.First();
                    groupedUsers.Roles= g.Select(u => u.Roles.Single()).ToList();
                    return groupedUsers;
                });

                return result.ToList();
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            try
            {
                var sql = "SELECT Id, Name, Email, Bio, Image, Slug, PasswordHash FROM UserBlog WHERE Id = @Id";
                var user = await _connection.QueryFirstOrDefaultAsync<User>(sql, new { id });
                return user;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateUserAsync(User user, int id)
        {
            try
            {
                var sql = "UPDATE UserBlog SET Name = @Name, Email = @Email, Bio = @Bio, Image = @Image, PasswordHash = @PasswordHash, Slug = @Slug WHERE Id = @Id";
                await _connection.ExecuteAsync(sql, new { user.Name, user.Email, user.Bio, user.Image, user.PasswordHash, user.Slug, id });
            } catch(SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
