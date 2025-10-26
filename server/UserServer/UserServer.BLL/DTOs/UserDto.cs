using UserServer.DAL.Models;

namespace UserServer.BLL.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public ICollection<GetUserProjectAllDto> Projects { get; set; }
        public ICollection<Permission> Permissions { get; set; }
    }
}
