namespace Blog.API.Models.DTOs.Tag
{
    public class GetPostsTagsDTO
    {
        public int TagId { get; set; }
        public string TagName { get; set; }
        public string TagSlug { get; set; }
    }
}
