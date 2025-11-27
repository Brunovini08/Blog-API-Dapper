using Blog.API.Models;
using Blog.API.Models.DTOs.Category;
using Blog.API.Models.DTOs.Role;
using Blog.API.Services;
using Blog.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private RoleService _roleService;
        public RoleController(RoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet()]
        public async Task<ActionResult<List<RoleResponseDTO>>> GetAllRolesAsync()
        {
            try
            {
                var roles = await _roleService.GetAllRolesAsync();
                if(roles is null)
                    return NotFound("Nenhuma role registrada");
                return Ok(roles);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRoleById(int id)
        {
            try
            {
                var role = await _roleService.GetByIdRoleAsync(id);
                if (role is null)
                    return NotFound("Role não encontrada");
                return Ok(role);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost()]
        public async Task<ActionResult> CreateRole([FromBody] RoleRequestDTO role)
        {
            try
            {
                await _roleService.CreateRoleAsync(role);
                return Created();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRole(int id)
        {
            try
            {   
                await _roleService.DeleteRoleAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRole(RoleRequestDTO role, int id)
        {
            try
            {
                await _roleService.UpdateRoleAsync(role, id);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
