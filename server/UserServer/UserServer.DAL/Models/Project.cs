namespace UserServer.DAL.Models
{
    public class Project : BaseEntity
    {
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public Guid CreatedBy { get; set; }

        public string Status { get; set; }
        // Navigation Properties
        public ICollection<User> Users { get; set; }
        public ICollection<CADFile> CADFiles { get; set; }
        public ICollection<Permission> Permissions { get; set; }
        public ICollection<UserProjects> UserProjects { get; set; }
    }
}
