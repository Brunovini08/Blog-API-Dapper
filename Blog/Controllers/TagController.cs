using Blog.API.Models;
using Blog.API.Models.DTOs.Role;
using Blog.API.Models.DTOs.Tag;
using Blog.API.Services;
using Blog.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private TagService _tagService;
        public TagController(TagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet()]
        public async Task<ActionResult<List<RoleResponseDTO>>> GetAllTagsAsync()
        {
            try  
            {
                var roles = await _tagService.GetAllTagsAsync();
                if (roles is null)
                    return NotFound("Não existe tags registradas");
                return Ok(roles);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetTagById(int id)
        {
            try
            {
                var role = await _tagService.GetByIdTagAsync(id);
                if (role is null)
                    return NotFound("Tag não encontrada");
                return Ok(role);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost()]
        public async Task<ActionResult> CreateTag([FromBody] TagRequestDTO tag)
        {
            try
            {
                await _tagService.CreateTagAsync(tag);
                return Created();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTag(int id)
        {
            try
            {
                await _tagService.DeleteTagAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTag(TagRequestDTO tag, int id)
        {
            try
            {
                await _tagService.UpdateTagAsync(tag, id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
