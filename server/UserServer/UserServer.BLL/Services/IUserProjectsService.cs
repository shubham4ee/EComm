using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserServer.BLL.Services
{
    public interface IUserProjectsService
    {
        Task<bool> AssignUsersToProjectAsync(Guid pid, Guid[] uids);
        Task<bool> AssignProjectsToUserAsync(Guid uid, Guid[] pids);

    }
}
