using Blog.API.Models;
using Blog.API.Models.DTOs.Post;
using Blog.API.Repositories;
using Blog.API.Repositories.Interfaces;
using Blog.API.Services.Interfaces;

namespace Blog.API.Services
{
    public class PostService : IPostService
    {
        private PostRepository _postRepository { get; set; }
        public PostService(PostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        public async Task CreatePostAsync(PostRequestDTO post, int authorId, int categoryId)
        {
            try
            {
                var newPost = new Post(categoryId, authorId, post.Title, post.Summary, post.Body, post.Title.ToLower().Replace(" ", "-") + "-" + DateTime.Now.ToString().Replace("/", "-"));
                await _postRepository.CreatePostAsync(newPost, post.TagsId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeletePostAsync(int id)
        {
           try
            {
                await _postRepository.DeletePostAsync(id);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<List<PostResponseDTO>> GetAllPostsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<GetAllPostsWithTagsDTO>> GetAllPostsWithTagsAsync()
        {
            try
            {
                return await _postRepository.GetAllPostWithTagsAsync();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task GetByIdPostAsync(int id)
        {
            //try
            //{
            //    return await _postRepository.GetPostByIdAsync(id);
            //} catch(Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}
            
        }

        public async Task UpdatePostAsync(PostRequestUpdateDTO post, int id)
        {
            //try
            //{
            //    var postStorage = await this.GetByIdPostAsync(id);
            //    if (postStorage is null)
            //        throw new Exception("Post não encontrado");
            //    if (post.Title != null)
            //    {
            //        postStorage.set(user.Name);
            //        userStorage.setSlug(user.Name.ToLower().Replace(" ", "-") + "-" + new DateOnly().ToString().Replace("/", "-"));
            //    }
            //    if (user.Image != null)
            //    {
            //        userStorage.setImage(user.Image);
            //    }
            //    if (user.Email != null)
            //    {
            //        userStorage.setEmail(user.Email);
            //    }
            //    if (user.Password != null)
            //    {
            //        userStorage.setPasswordHash(HashPassword(user.Password));
            //    }
            //    if (user.Bio != null)
            //    {
            //        userStorage.setBio(user.Bio);
            //    }

            //    await _userRepository.UpdateUserAsync(userStorage, id);
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}
        }
    }
}
