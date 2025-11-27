using Blog.API.Data;
using Blog.API.Models;
using Blog.API.Models.DTOs.Role;
using Blog.API.Repositories;
using Blog.API.Repositories.Interfaces;
using Blog.API.Services.Interfaces;
using Microsoft.Data.SqlClient;

namespace Blog.API.Services
{
    public class RoleService : IRoleService
    {
        private RoleRepository _roleRepository { get; set; }
        public RoleService(RoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public async Task CreateRoleAsync(RoleRequestDTO role)
        {
            try
            {
                var newRole = new Role(role.Name, role.Name.ToLower().Replace(" ", "-"));
                await _roleRepository.CreateRoleAsync(newRole);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteRoleAsync(int id)
        {
            try
            {
                await _roleRepository.DeleteRoleAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<RoleResponseDTO>> GetAllRolesAsync()
        {
            try
            {
                return await _roleRepository.GetAllRolesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Role> GetByIdRoleAsync(int id)
        {
            try
            {
                return await _roleRepository.GetRoleByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateRoleAsync(RoleRequestDTO role, int id)
        {
            try
            {
                var newRole = new Role(role.Name, role.Name.ToLower().Replace(" ", "-"));
                await _roleRepository.UpdateRoleAsync(newRole, id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
