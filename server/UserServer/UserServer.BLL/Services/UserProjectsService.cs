using UserServer.DAL.Models;
using UserServer.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserServer.BLL.Services
{
    public class UserProjectsService : IUserProjectsService
    {
        public readonly IUserProjectsRepository _userProjectsRepository;
        public UserProjectsService(IUserProjectsRepository userProjectsRepository) 
        {
           _userProjectsRepository = userProjectsRepository;
        }


        public async Task<bool> AssignUsersToProjectAsync(Guid pid , Guid[] uids)
        {
            try
            {
                List<UserProjects> userProjects = new List<UserProjects>();

                foreach(Guid id in uids )
                {
                    UserProjects newUser = new UserProjects ();
                    newUser.UserId = id;
                    newUser.ProjectId = pid;
                    userProjects.Add (newUser); 
                }

                bool isAssigned = await _userProjectsRepository.AssignUsersToProjectsAsync(userProjects);

                return isAssigned;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> AssignProjectsToUserAsync(Guid uid, Guid[] pids)
        {
            try
            {
                List<UserProjects> userProjects = new List<UserProjects>();

                foreach (Guid id in pids)
                {
                    UserProjects newUser = new UserProjects();
                    newUser.UserId = uid;
                    newUser.ProjectId = id;
                    userProjects.Add(newUser);
                }

                bool isAssigned = await _userProjectsRepository.AssignUsersToProjectsAsync(userProjects);

                return isAssigned;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
