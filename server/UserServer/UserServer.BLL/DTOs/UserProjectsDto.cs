namespace UserServer.BLL.DTOs
{
    public class UserProjectsDto
    {
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }
        public DateTime AssignedDate { get; set; }
    }
}
