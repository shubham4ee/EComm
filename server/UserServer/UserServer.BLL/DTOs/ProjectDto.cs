using UserServer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserServer.BLL.DTOs
{
    internal class ProjectDto
    {
        public Guid Id { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public ICollection<CADFile> CADFiles { get; set; }
        public ICollection<UserDto> Users { get; set; }
    }
}
