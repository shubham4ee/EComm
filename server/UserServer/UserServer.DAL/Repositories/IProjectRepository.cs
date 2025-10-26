using UserServer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserServer.DAL.Repositories
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        Task<IEnumerable<Project>> GetAllProjectsByPageIndexAsync(int pageIndex, int pageSize);
        Task<IEnumerable<Project>> GetProjectsByUserIdAsync(Guid userId);
        Task<IEnumerable<Project>> GetProjectsByProjectIdAsync(Guid Pid);
    }
}
