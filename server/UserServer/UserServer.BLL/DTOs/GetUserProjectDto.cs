using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserServer.BLL.DTOs
{
    public class GetUserProjectDto
    {
        public Guid Id { get; set; }
        public required string ProjectName { get; set; }
        public string Status { get; set; }
        public required string Description { get; set; }
        public required ICollection<GetCADFileDTO> CADFiles { get; set; }
    }
}
