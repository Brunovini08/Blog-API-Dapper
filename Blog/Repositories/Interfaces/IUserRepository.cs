using Blog.API.Models;
using Blog.API.Models.DTOs.Tag;
using Blog.API.Models.DTOs.User;

namespace Blog.API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<List<UserResponseDTO>> GetAllUserAsync();
        public Task<User> GetUserByIdAsync(int id);
        public Task CreateUserAsync(User user);
        public Task  UpdateUserAsync(User user, int id);
        public Task DeleteUserAsync(int id);
        public Task<List<GetAllUsersWithRoleDTO>> GetAllUsersWithRoleAsync();
    }
}
