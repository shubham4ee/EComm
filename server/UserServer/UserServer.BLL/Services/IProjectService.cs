using UserServer.BLL.DTOs;
using UserServer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserServer.BLL.Services
{
    public interface IProjectService
    {
        Task<bool> CreateProjectAsync(CreateProjectDto project, string createdBy);
        Task<IEnumerable<GetAdminProjectDto>> GetAllProjectsAsync(int pageIndex, int pageSize);
        Task<IEnumerable<GetUserProjectDto>> GetProjectsbyUserIdAsync(Guid UId);
        Task<Object> GetProjectbyIdAsync(Guid Id, String Role);
        Task<bool> UpdateProjectAsync(Guid Id, UpdateProjectDto project);
        Task<bool> DeleteProjectAsync(Guid Id);
    }
}
