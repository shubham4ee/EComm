using UserServer.DAL.Models;

namespace UserServer.BLL.DTOs
{
    public class GetUserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
