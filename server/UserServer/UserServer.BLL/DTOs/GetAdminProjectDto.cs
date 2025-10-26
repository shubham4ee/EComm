using UserServer.DAL.Models;

namespace UserServer.BLL.DTOs
{
    public class GetAdminProjectDto
    {
        public Guid Id { get; set; }
        public required string ProjectName { get; set; }
        public string Status { get; set; }
        public required string Description { get; set; }
        public Guid CreatedBy { get; set; }
        public required ICollection<GetCADFileDTO> CADFiles { get; set; }
        public required ICollection<GetUserDto> Users { get; set; }
    }
}
