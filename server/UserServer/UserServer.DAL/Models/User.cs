using System.Text.Json.Serialization;

namespace UserServer.DAL.Models
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        // Navigation Properties
       
        public ICollection<Project> Projects { get; set; }
        public ICollection<Permission> Permissions { get; set; }
        public ICollection<UserProjects> UserProjects { get; set; }
    }
}
