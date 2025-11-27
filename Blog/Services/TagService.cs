using Blog.API.Models;
using Blog.API.Models.DTOs.Tag;
using Blog.API.Repositories;
using Blog.API.Repositories.Interfaces;
using Blog.API.Services.Interfaces;
using System.Data;

namespace Blog.API.Services
{
    public class TagService : ITagService
    {
        private TagRepository _tagRepository { get; set; }
        public TagService(TagRepository tagRepository)
        {
            _tagRepository = tagRepository;  
        }
        public async Task CreateTagAsync(TagRequestDTO tag)
        {
            try
            {
                var newTag = new Tag(tag.Name, tag.Name.ToLower().Replace(" ", "-"));
                await _tagRepository.CreateTagAsync(newTag);
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
                await _tagRepository.DeleteTagAsync(id);
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
                return await _tagRepository.GetAllTagsAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Tag> GetByIdTagAsync(int id)
        {
            try
            {
                return await _tagRepository.GetTagByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateTagAsync(TagRequestDTO tag, int id)
        {
            try
            {
                var newRole = new Tag(tag.Name, tag.Name.ToLower().Replace(" ", "-"));
                await _tagRepository.UpdateTagAsync(newRole, id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
