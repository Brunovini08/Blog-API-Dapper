using Blog.API.Models;
using Blog.API.Models.DTOs.Post;
using Blog.API.Models.DTOs.User;

namespace Blog.API.Services.Interfaces
{
    public interface IPostService
    {
        public Task<List<PostResponseDTO>> GetAllPostsAsync();
        public Task<List<GetAllPostsWithTagsDTO>> GetAllPostsWithTagsAsync();
        public Task GetByIdPostAsync(int id);
        public Task CreatePostAsync(PostRequestDTO post, int authorId, int categoryId);
        public Task UpdatePostAsync(PostRequestUpdateDTO post, int id);
        public Task DeletePostAsync(int id);
    }
}
