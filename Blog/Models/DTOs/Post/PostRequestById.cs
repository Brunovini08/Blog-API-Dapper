using Blog.API.Models.DTOs.Tag;

namespace Blog.API.Models.DTOs.Post
{
    public class PostRequestById
    {
        public int PostId { get; private set; }
        public int CategoryId { get; private set; }
        public int AuthorId { get; private set; }
        public string Title { get; private set; }
        public string Summary { get; private set; }
        public string Body { get; private set; }
        public string Slug { get; private set; }
        public DateTime CreateDate { get; private set; }
        public DateTime LastUpdateDate { get; private set; }
        public List<GetPostsTagsDTO> Tags { get; set; } = new();
    }
}
