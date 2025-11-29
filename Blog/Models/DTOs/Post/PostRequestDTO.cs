namespace Blog.API.Models.DTOs.Post
{
    public class PostRequestDTO
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Body { get; set; }
        public List<int> TagsId { get; set; } = [];
    }
}
