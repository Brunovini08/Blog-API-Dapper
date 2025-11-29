using Blog.API.Models;
using Blog.API.Models.DTOs.Tag;

namespace Blog.API.Models.DTOs.Post
{
    public class GetAllPostsWithTagsDTO
    {
        public int PostId { get; set; }
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; }

        public List<GetPostsTagsDTO> Tags { get; set; } = new();
    }
}
