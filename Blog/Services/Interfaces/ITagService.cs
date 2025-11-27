using Blog.API.Models;
using Blog.API.Models.DTOs.Role;
using Blog.API.Models.DTOs.Tag;

namespace Blog.API.Services.Interfaces
{
    public interface ITagService
    {
        public Task<List<TagResponseDTO>> GetAllTagsAsync();
        public Task<Tag> GetByIdTagAsync(int id);
        public Task CreateTagAsync(TagRequestDTO tag);
        public Task UpdateTagAsync(TagRequestDTO tag, int id);
        public Task DeleteTagAsync(int id);
    }
}
