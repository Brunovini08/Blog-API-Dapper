using Blog.API.Models.DTOs.Tag;
using Blog.API.Models.DTOs.User;
using Blog.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }
        [HttpPost()]
        public async Task<ActionResult> CreateUser([FromBody] UserRequestDTO user)
        {
            try
            {
                await _userService.CreateUserAsync(user);
                return Created();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        [HttpGet()]
        public async Task<ActionResult> GetAllUser()
        {
            try
            {
                var users = await _userService.GetAllUsersWithRoleAsync();
                if(users is null)
                    return NotFound("Nenhum usuário registrado");
                return Ok(users);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUserById(int id)
        {
            try
            {
                var user = await _userService.GetByIdUserAsync(id);
                if (user is null)
                    return NotFound("Nenhum usuário registrado");
                return Ok(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser([FromBody] UserRequestUpdateDTO user, int id)
        { 
            try
            {
                
                await _userService.UpdateUserAsync(user, id);
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

                await _userService.DeleteUserAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("convert-image")]
        public async Task<ActionResult> ConvertImageToBase64(IFormFile image)
        {
            try
            {
                using var ms = new MemoryStream();
                await image.CopyToAsync(ms);
                var imageUser = Convert.ToBase64String(ms.ToArray());
                return Ok(imageUser);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
