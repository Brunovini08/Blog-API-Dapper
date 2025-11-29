namespace Blog.API.Models.DTOs.User
{
    public class UserRequestUpdateDTO
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Bio { get; set; }
        public string? Image { get; set; }
    }
}
