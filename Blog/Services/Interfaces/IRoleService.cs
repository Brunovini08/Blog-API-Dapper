using Blog.API.Models;
using Blog.API.Models.DTOs.Category;
using Blog.API.Models.DTOs.Role;

namespace Blog.API.Services.Interfaces
{
    public interface IRoleService
    {
        public Task<List<RoleResponseDTO>> GetAllRolesAsync();
        public Task<Role> GetByIdRoleAsync(int id);
        public Task CreateRoleAsync(RoleRequestDTO role);
        public Task UpdateRoleAsync(RoleRequestDTO role, int id);
        public Task DeleteRoleAsync(int id);
    }
}
