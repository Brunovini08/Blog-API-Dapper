using Blog.API.Models.DTOs.Post;
using Blog.API.Models.DTOs.Role;
using Blog.API.Models.DTOs.User;
using Blog.API.Services;
using Blog.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private PostService _postService;
        public PostController(PostService postService)
        {
            _postService = postService;
        }

        [HttpPost("{authorId}/{categoryId}")]
        public async Task<ActionResult> CreateUser([FromBody] PostRequestDTO post, int authorId, int categoryId)
        {
            try
            {
                await _postService.CreatePostAsync(post, authorId, categoryId);
                return Created();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpGet()]
        public async Task<ActionResult> GetAllRolesAsync()
        {
            try
            {
                var posts = await _postService.GetAllPostsWithTagsAsync();
                if (posts is null)
                    return NotFound("Nenhum post registrado");
                return Ok(posts);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetPostById(int id)
        {
            //try
            //{
            //    await _postService.GetByIdPostAsync(id);
            //    if (user is null)
            //        return NotFound("Post não encontrado");
            //    return Ok(user);
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePost([FromBody] PostRequestUpdateDTO post, int id)
        {
            try
            {

                await _postService.UpdatePostAsync(post, id);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {

                await _postService.DeletePostAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
