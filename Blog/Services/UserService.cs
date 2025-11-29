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
                var newUser = new User(user.Name, user.Email, hashPassword, user.Bio, user.Image, user.Name.ToLower().Replace(" ", "-") + "-" + new DateOnly().ToString().Replace("/", "-"));
                await _userRepository.CreateUserAsync(newUser);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteUserAsync(int id)
        {
           try
            {
               await _userRepository.DeleteUserAsync(id);
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
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

        public async Task<User> GetByIdUserAsync(int id)
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

        public async Task UpdateUserAsync(UserRequestUpdateDTO user, int id)
        {
            try
            {
                var userStorage = await this.GetByIdUserAsync(id);
                if (userStorage is null)
                    throw new Exception("Usuário não encontrado");
                if(user.Name != null)
                {
                    userStorage.setName(user.Name);
                    userStorage.setSlug(user.Name.ToLower().Replace(" ", "-") + "-" + new DateOnly().ToString().Replace("/", "-"));
                } 
                if(user.Image != null)
                {
                    userStorage.setImage(user.Image);
                }
                if(user.Email != null)
                {
                    userStorage.setEmail(user.Email);
                }
                if(user.Password != null)
                {
                    userStorage.setPasswordHash(HashPassword(user.Password));
                }
                if(user.Bio != null)
                {
                    userStorage.setBio(user.Bio);
                }

               await _userRepository.UpdateUserAsync(userStorage, id);
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<GetAllUsersWithRoleDTO>> GetAllUsersWithRoleAsync()
        {
            try
            {
                return await _userRepository.GetAllUsersWithRoleAsync();
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
