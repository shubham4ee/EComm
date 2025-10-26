using UserServer.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserServer.BLL.Services
{
    public interface ICADFileService
    {
        Task<bool> CreateCADFiles(Guid pid, Guid uid, string[] fileNames);
        Task<GetCADFileDTO> DeleteCADFile(Guid fid);
    }
}
