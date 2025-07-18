namespace server.DTOs.User
{
    public class UserCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; // hashed in service
    }
}