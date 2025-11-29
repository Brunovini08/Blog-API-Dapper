namespace Blog.API.Models.DTOs.Post
{
    public class PostRequestUpdateDTO
    {
        public string Title { get; private set; }
        public string Summary { get; private set; }
        public string Body { get; private set; }
        public string Slug { get; private set; }
    }
}
