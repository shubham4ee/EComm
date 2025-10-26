using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserServer.BLL.Services
{
    public interface IMinioService
    {
        Task<List<string>> GetDownloadUrls(string pid, Guid[] fIds);
        Task<List<string>> GetUploadUrls(string pid, string[] fileNames);
    }
}
