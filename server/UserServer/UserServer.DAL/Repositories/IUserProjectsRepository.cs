using UserServer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserServer.DAL.Repositories
{
    public interface IUserProjectsRepository 
    {
         Task<bool> AssignUsersToProjectsAsync(List<UserProjects> userProjects);
    }
}
