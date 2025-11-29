using Blog.API.Models;
using Blog.API.Models.DTOs.Post;
using Blog.API.Models.DTOs.User;

namespace Blog.API.Repositories.Interfaces
{
    public interface IPostRepository
    {
        public Task<List<PostResponseDTO>> GetAllPostAsync();
        public Task<PostRequestById> GetPostByIdAsync(int id);
        public Task CreatePostAsync(Post post, List<int> tagsId);
        public Task UpdatePostAsync(Post post, int id);
        public Task DeletePostAsync(int id);
        public Task<List<GetAllPostsWithTagsDTO>> GetAllPostWithTagsAsync();
    }
}
