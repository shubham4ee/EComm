using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserServer.BLL.DTOs
{
    public class CreateCADFileDto
    {
        public Guid ProjectId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public Guid UploadedBy { get; set; }
     
    }
}
