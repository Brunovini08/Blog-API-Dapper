using Blog.API.Models;
using Blog.API.Models.DTOs.Tag;
using Blog.API.Models.DTOs.User;

namespace Blog.API.Services.Interfaces
{
    public interface IUserService
    {
        public Task<List<UserResponseDTO>> GetAllUsersAsync();
        public Task<List<GetAllUsersWithRoleDTO>> GetAllUsersWithRoleAsync();
        public Task<User> GetByIdUserAsync(int id);
        public Task CreateUserAsync(UserRequestDTO user);
        public Task UpdateUserAsync(UserRequestUpdateDTO user, int id);
        public Task DeleteUserAsync(int id);
    }
}
