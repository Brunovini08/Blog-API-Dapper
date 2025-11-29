using Blog.API.Data;
using Blog.API.Models;
using Blog.API.Models.DTOs.Post;
using Blog.API.Models.DTOs.Role;
using Blog.API.Models.DTOs.Tag;
using Blog.API.Models.DTOs.User;
using Blog.API.Repositories.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;

namespace Blog.API.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly SqlConnection _connection;
        public PostRepository(ConnectionDB connectionDB)
        {
            _connection = connectionDB.GetConnection();
        }

        public async Task CreatePostAsync(Post post, List<int> tagsId)
        {
            try
            {
                var sql = "INSERT INTO Post (CategoryId, AuthorId, Title, Summary, Body, Slug, CreateDate, LastUpdateDate) VALUES (@CategoryId, @AuthorId, @Title, @Summary, @Body, @Slug, @CreateDate, @LastUpdateDate); SELECT CAST(SCOPE_IDENTITY() AS INT)";
                int id = await _connection.ExecuteScalarAsync<int>(sql, new { post.CategoryId, post.AuthorId, post.Title, post.Summary, post.Body, post.Slug, post.CreateDate, post.LastUpdateDate });
                var sqlUserRole = "INSERT INTO PostTag(PostId, TagId) VALUES (@PostId, @TagId)";
                var postTags = tagsId.Select(tagId => new { PostId = id, TagId = tagId });
                await _connection.ExecuteAsync(sqlUserRole, postTags);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeletePostAsync(int id)
        {
            try
            {
                var sqlPostTag = @"DELETE FROM PostTag WHERE PostId = @Id";
                await _connection.ExecuteAsync(sqlPostTag, new { id });

                var sql = @"DELETE FROM Post WHERE Id = @Id";
                await _connection.ExecuteAsync(sql, new { id });
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<List<PostResponseDTO>> GetAllPostAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<GetAllPostsWithTagsDTO>> GetAllPostWithTagsAsync()
        {
            try
            {

                //verificar se a tag existe!
                var sql = @"
                            SELECT 
                                p.Id AS PostId,
                                p.CategoryId,
                                p.AuthorId,
                                p.Title,
                                t.Id AS TagId,
                                t.Name AS TagName,
                                t.Slug AS TagSlug
                            FROM Post p
                            INNER JOIN PostTag pt ON pt.PostId = p.Id
                            INNER JOIN Tag t ON t.Id = pt.TagId";
                var posts = await _connection.QueryAsync<GetAllPostsWithTagsDTO, GetPostsTagsDTO, GetAllPostsWithTagsDTO>(sql, (post, tag) =>
                {
                    post.Tags.Add(tag);
                    return post;
                }, splitOn: "TagId");

                var result = posts.GroupBy(p => p.PostId).Select(g =>
                {
                    var groupedPost = g.First();
                    groupedPost.Tags = g.Select(p => p.Tags.Single()).ToList();
                    return groupedPost;
                });

                return result.ToList();
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PostRequestById> GetPostByIdAsync(int id)
        {
            var sql = @"
                            SELECT 
                                p.Id AS PostId,
                                p.CategoryId,
                                p.AuthorId,
                                p.Title,
                                p.Summary,
                                p.Body,
                                p.Slug,
                                p.CreateDate,
                                p.LastUpdateDate,
                                t.Id AS TagId,
                                t.Name AS TagName,
                                t.Slug AS TagSlug
                            FROM Post p
                            INNER JOIN PostTag pt ON pt.PostId = p.Id
                            INNER JOIN Tag t ON t.Id = pt.TagId WHERE PostId = @Id";
            var post = await _connection.QueryAsync<PostRequestById, GetPostsTagsDTO, PostRequestById>(sql, (post, tag) =>
            {
                post.Tags.Add(tag);
                return post;
            }, new { Id = id }, splitOn: "TagId");

            var result = post.GroupBy(p => p.PostId).Select(g =>
            {
                var groupedPost = g.First();
                groupedPost.Tags = g.Select(p => p.Tags.Single()).ToList();
                return groupedPost;
            });

            var postReturn = result.FirstOrDefault(p =>  p.PostId == id);
            return postReturn;
        }

        public async Task UpdatePostAsync(Post post, int id)
        {
            try
            {
                var sql = "UPDATE Post SET Title = @Title, Summary = @Summary, Body = @Body, Slug = @Slug, CreateDate = @CreateDate, LastUpdateDate = @LastUpdateDate WHERE Id = @Id";
                await _connection.ExecuteAsync(sql, new { Title = post.Title, Summary = post.Summary, Body = post.Body, Slug = post.Slug, CreateDate = post.CreateDate, LastUpdateDate = post.LastUpdateDate, Id = id });
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
