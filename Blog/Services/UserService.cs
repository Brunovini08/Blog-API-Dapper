using Azure;
using Blog.API.Models;
using Blog.API.Models.DTOs.User;
using Blog.API.Repositories;
using Blog.API.Services.Interfaces;
using Microsoft.Data.SqlClient;
using static BCrypt.Net.BCrypt;
namespace Blog.API.Services
{
    public class UserService : IUserService
    {
        private UserRepository _userRepository { get; set; }
        public UserService(UserRepository userRepository )
        {
            _userRepository = userRepository;
        }
        public async Task CreateUserAsync(UserRequestDTO user)
        {
            try
            {
                var hashPassword = HashPassword(user.Password);
                var newUser = new User(user.Name, user.Email, hashPassword, user.Bio, user.Image, user.Name.ToLower().Replace(" ", "-") + new DateOnly());
                await _userRepository.CreateUserAsync(newUser);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task DeleteUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserResponseDTO>> GetAllUsersAsync()
        {
            try
            {
                return await _userRepository.GetAllUserAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserResponseDTO> GetByIdUserAsync(int id)
        {
            try
            {
                return await _userRepository.GetUserByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task UpdateUserAsync(UserRequestDTO user, int id)
        {
            throw new NotImplementedException();
        }
    }
}
