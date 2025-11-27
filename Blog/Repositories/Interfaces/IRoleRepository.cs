using Blog.API.Models;
using Blog.API.Models.DTOs.Category;
using Blog.API.Models.DTOs.Role;

namespace Blog.API.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        public Task<List<RoleResponseDTO>> GetAllRolesAsync();
        public Task<Role> GetRoleByIdAsync(int id);
        public Task CreateRoleAsync(Role role);
        public Task UpdateRoleAsync(Role role, int id);
        public Task DeleteRoleAsync(int id);
    }
}
