namespace Blog.API.Models
{
    public class User
    {

        public User()
        {
            
        }
        public User(string name, string email, string passwordHash, string? bio, string image, string slug)
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            Bio = bio;
            Image = image;
            Slug = slug;
        }

        public void setName(string name) { Name = name; }
        public void setEmail(string email) { Email = email; }
        public void setPasswordHash(string passwordHash) { PasswordHash = passwordHash; }
        public void setBio(string bio) { Bio = bio; }
        public void setImage(string image) { Image = image; }
        public void setSlug(string slug) { Slug = slug; }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string? Bio { get; private set; }
        public string Image { get; private set; }
        public string Slug { get; private set; }    
        public List<Role> Roles { get; private set; } = new();
    }
}
