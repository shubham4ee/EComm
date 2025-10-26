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
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectRepository(ApplicationDbContext context) : base(context) 
        {
            _context = context;
        }


        public async Task<IEnumerable<Project>> GetAllProjectsByPageIndexAsync(int pageIndex, int pageSize)
        {
            try
            {
                return await _context
                    .Projects
                    .OrderByDescending(u => u.CreatedAt)
                    .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                    .Select(p => new Project
                    {
                        Id = p.Id,
                        ProjectName = p.ProjectName,
                        Description = p.Description,
                        CreatedBy = p.CreatedBy,
                        Status = p.Status,

                        Users = p.UserProjects
                                    .Where(up => up.ProjectId == p.Id)
                                    .Select(up => up.User)
                                    .ToList(),
                        CADFiles = p.CADFiles
                                    .Where(cf => cf.ProjectId == p.Id)
                                    .ToList()
                    })
                    .ToListAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("Database update failed while fetching projects.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error occurred in the repository layer.", ex);
            }
        }


        public async Task<IEnumerable<Project>> GetProjectsByUserIdAsync(Guid userId)
        {
            try
            {
                return await _context
                    .Projects
                    .Where(p => p.UserProjects.Any(up => up.UserId == userId))
                    .Select(p => new Project
                    {
                        Id = p.Id,
                        ProjectName = p.ProjectName,
                        Description = p.Description,
                        CADFiles = p.CADFiles,
                        Status = p.Status
                        
                    })
                    .ToListAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("Database error occurred while fetching user projects.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error occurred in the repository layer.", ex);
            }
        }

        public async Task<IEnumerable<Project>> GetProjectsByProjectIdAsync(Guid Pid)
        {
            try
            {
                return await _context
                    .Projects
                    .Where(p => p.Id == Pid)
                    .Select(p => new Project
                    {

                        Id = p.Id,
                        ProjectName = p.ProjectName,
                        Description = p.Description,
                        CreatedBy = p.CreatedBy,
                        Status = p.Status,

                        Users = p.UserProjects
                                    .Where(up => up.ProjectId == p.Id)
                                    .Select(up => up.User)
                                    .ToList(),
                        CADFiles = p.CADFiles
                                    .Where(cf => cf.ProjectId == p.Id)
                                    .ToList()

                    })
                    .ToListAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("Database error occurred while fetching user projects.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error occurred in the repository layer.", ex);
            }
        }
    }
}
