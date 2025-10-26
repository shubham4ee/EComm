using AutoMapper;
using UserServer.BLL.DTOs;
using UserServer.DAL.Models;
using UserServer.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserServer.BLL.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        public readonly IMapper _mapper;

        public ProjectService(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreateProjectAsync(CreateProjectDto projectDto, string createdBy)
        {
            if (projectDto == null)
                throw new ArgumentNullException(nameof(projectDto), "Project data cannot be null.");

            try
            {
                // Map DTO to entity
                var project = _mapper.Map<Project>(projectDto);
                project.CreatedBy = Guid.Parse(createdBy);
                // Add project to repository
                await _projectRepository.AddAsync(project);

                return true;
            }
            catch (DbUpdateException ex)
            {
                // Handle database-specific errors
                throw new Exception("Database error occurred while creating the project.", ex);
            }
            catch (Exception ex)
            {
                // Catch all other exceptions
                throw new Exception("An unexpected error occurred in the service layer.", ex.InnerException);
            }
        }


        public async Task<IEnumerable<GetAdminProjectDto>> GetAllProjectsAsync(int pageIndex, int pageSize)
        {
            try
            {
                // Fetch all projects from the repository
                var projects = await _projectRepository.GetAllProjectsByPageIndexAsync(pageIndex, pageSize);

                // Map to DTOs
                return _mapper.Map<IEnumerable<GetAdminProjectDto>>(projects);
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception("The requested data is invalid or null.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error occurred while fetching projects.", ex);
            }
        }


        public async Task<IEnumerable<GetUserProjectDto>> GetProjectsbyUserIdAsync(Guid UId)
        {
            try
            {
                var projects = await _projectRepository.GetProjectsByUserIdAsync(UId);

                if (projects == null || !projects.Any())
                {
                    throw new KeyNotFoundException("No projects found for the given User ID.");
                }

                return _mapper.Map<IEnumerable<GetUserProjectDto>>(projects);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException("No projects found for the given User ID.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error occurred while fetching user projects.", ex);
            }
        }


        public async Task<Object> GetProjectbyIdAsync(Guid PId, string Role="user")
        {
            if (PId == Guid.Empty)
            {
                throw new ArgumentException("Project ID cannot be empty.");
            }

            try
            {

                IEnumerable<Project> project = await _projectRepository.GetProjectsByProjectIdAsync(PId);

                // Map the project entity to DTO
                Console.WriteLine(project);
                if(Role == "admin")
                {
                    var projectDTO = _mapper.Map<IEnumerable<GetAdminProjectDto>>(project);
                    return projectDTO;
                }
                else
                {
                    var projectDTO = _mapper.Map< IEnumerable<GetUserProjectDto>>(project);
                    return projectDTO;
                }
                    
            }
            catch (KeyNotFoundException ex)
            {
                // Rethrow specific exceptions to be handled by the controller
                throw new ApplicationException(ex.Message);
            }
            catch (DbUpdateException ex)
            {
                // Log database-specific errors (optional)
                //_logger.LogError(ex, "Database error occurred while fetching the project by ID.");
                throw new InvalidOperationException("A database error occurred while processing the request.", ex);
            }
            catch (Exception ex)
            {
                // Log unexpected errors (optional)
                //_logger.LogError(ex, "An unexpected error occurred while fetching the project by ID.");
                throw new InvalidOperationException("An unexpected error occurred. Please try again later.", ex);
            }
        }

        public async Task<bool> UpdateProjectAsync(Guid id, UpdateProjectDto projectDto)
        {
            try
            {
                var project = await _projectRepository.GetByIdAsync(id);

                if (project == null)
                {
                    return false; // Project not found
                }

                // Map updated fields to the project entity
                project = _mapper.Map(projectDto, project);
                // Update the ModifiedAt field
                project.ModifiedAt = DateTime.UtcNow;
                // Save changes to the database
                await _projectRepository.UpdateAsync(project);
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("Database error occurred while updating the project.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error occurred while updating the project.", ex);
            }
        }

        public async Task<bool> DeleteProjectAsync(Guid id)
        {
            try
            {
                var project = await _projectRepository.GetByIdAsync(id);

                if (project == null)
                {
                    return false; // Project not found
                }

                await _projectRepository.DeleteAsync(project);
                return true; // Project deleted successfully
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("Database error occurred while deleting the project.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error occurred while deleting the project.", ex);
            }
        }


    }
}
