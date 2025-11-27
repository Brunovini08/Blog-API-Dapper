namespace Blog.API.Models.DTOs.User
{
    public class UserResponseDTO
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string? Bio { get; private set; }
        public string Image { get; private set; }
        public string Slug { get; private set; }
    }
}
