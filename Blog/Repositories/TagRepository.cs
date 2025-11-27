using Blog.API.Data;
using Blog.API.Models;
using Blog.API.Models.DTOs.Role;
using Blog.API.Models.DTOs.Tag;
using Blog.API.Repositories.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace Blog.API.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly SqlConnection _connection;
        public TagRepository(ConnectionDB connectionDB)
        {
            _connection = connectionDB.GetConnection();
        }
        public async Task CreateTagAsync(Tag tag)
        {
            try
            {
                var sql = "INSERT INTO Tag (Name, Slug) VALUES (@Name, @Slug)";
                await _connection.ExecuteAsync(sql, new { tag.Name, tag.Slug });
            }
            catch(SqlException ex)
            {

                if (ex.Message.Contains("Violation of UNIQUE KEY"))
                    throw new Exception("Já existe uma tag com esse nome");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteTagAsync(int id)
        {
            try
            {
                var sql = "DELETE Tag WHERE Id = @Id";
                await _connection.ExecuteAsync(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<TagResponseDTO>> GetAllTagsAsync()
        {
            try
            {
                var sql = "SELECT Name, Slug FROM Tag";
                var tags = (await _connection.QueryAsync<TagResponseDTO>(sql)).ToList();
                return tags;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Tag> GetTagByIdAsync(int id)
        {
            try
            {
                var sql = "SELECT * FROM Tag WHERE Id = @Id";
                var tag = await _connection.QueryFirstOrDefaultAsync<Tag>(sql, new { id });
                return tag;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateTagAsync(Tag tag, int id)
        {
            try
            {
                var sql = "UPDATE Tag SET Name = @Name, Slug = @Slug WHERE Id = @Id";
                await _connection.ExecuteAsync(sql, new { tag.Name, tag.Slug, id });
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("Violation of UNIQUE KEY"))
                    throw new Exception("Já existe uma tag com esse nome");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
