namespace UserServer.DAL.Models
{
    public class Permission : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }
        public bool CanView { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }

        // Navigation Properties
        public User User { get; set; }
        public Project Project { get; set; }
    }

}
