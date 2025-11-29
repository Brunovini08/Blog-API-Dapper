namespace Blog.API.Models
{
    public class Post
    {
        public Post(int categoryId, int authorId, string title, string summary, string body, string slug)
        {
            CategoryId = categoryId;
            AuthorId = authorId;
            Title = title;
            Summary = summary;
            Body = body;
            Slug = slug;
            CreateDate = DateTime.Now;
            LastUpdateDate = DateTime.Now; 
        }

        public int Id { get; private set; }
        public int CategoryId { get; private set; }
        public int AuthorId { get; private set; }
        public string Title { get; private set; }
        public string Summary { get; private set; }
        public string Body { get; private set; }    
        public string Slug { get; private set; }
        public DateTime CreateDate  { get; private set; }
        public DateTime LastUpdateDate { get; private set; }
        public List<Tag> Tags { get; private set; } = new();

        public void setTitle(string title) { Title = title; }
        public void setSummary(string summary) { Summary = summary; }
        public void setBody(string body) { Body = body; }
        public void setSlug(string slug) { Slug = slug; }

    }
}
