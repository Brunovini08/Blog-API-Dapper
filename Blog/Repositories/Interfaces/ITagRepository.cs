using Blog.API.Models;
using Blog.API.Models.DTOs.Role;
using Blog.API.Models.DTOs.Tag;

namespace Blog.API.Repositories.Interfaces
{
    public interface ITagRepository
    {
        public Task<List<TagResponseDTO>> GetAllTagsAsync();
        public Task<Tag> GetTagByIdAsync(int id);
        public Task CreateTagAsync(Tag tag);
        public Task UpdateTagAsync(Tag tag, int id);
        public Task DeleteTagAsync(int id);
    }
}
