using UserServer.DAL.DataContext;
using UserServer.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserServer.DAL.Repositories
{
    public class UserProjectsRepository :  IUserProjectsRepository
    {
        private readonly ApplicationDbContext _context;
        public UserProjectsRepository(ApplicationDbContext context) 
        {
            _context = context;
        }
        public async Task<bool> AssignUsersToProjectsAsync(List<UserProjects> userProjects)
        {
            try
            {
                List<UserProjects> newUsperProject = new List<UserProjects>();

                foreach( UserProjects up in userProjects)
                {
                    if( !_context.UserProjects.Contains(up))
                    {
                        newUsperProject.Add(up);
                    }
                }


                if (!newUsperProject.Any())
                    return false;
                await _context.UserProjects.AddRangeAsync(newUsperProject);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
                throw ex;
            }
        }
    }
}
