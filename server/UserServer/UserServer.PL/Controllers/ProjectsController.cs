using AutoMapper;
using UserServer.BLL.Services;
using UserServer.BLL.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using System.Security.Claims;


namespace UserServer.BLL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]

    public class ProjectsController : ControllerBase
    {

        private readonly APIResponse _apiResponse;
        private readonly IMapper _automapper;
        private readonly IProjectService _projectService;
        private readonly IUserProjectsService _userProjectsService;

        public ProjectsController(IProjectService porjectService, IMapper automapper , IUserProjectsService userProjectsService)
        {
            _automapper = automapper;
            _apiResponse = new APIResponse();
            _projectService = porjectService;
            _userProjectsService = userProjectsService;

        }




        //[Authorize(AuthenticationSchemes = "LoginForLocalUser", Roles = "admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("create")]
        public async Task<IActionResult> CreateProject(CreateProjectDto projectDto)
        {
            if (projectDto == null)
            {
                return BadRequest(new APIResponse
                {
                    StatusCode = 400,
                    Success = false,
                    Message = "Project data is required.",
                    Error = new
                    {
                        Code = "INVALID_INPUT",
                        Description = "The provided project data is null or invalid."
                    }
                });
            }

            try
            {
                // Validate required fields
                if (string.IsNullOrWhiteSpace(projectDto.ProjectName))
                {
                    return BadRequest(new APIResponse
                    {
                        StatusCode = 400,
                        Success = false,
                        Message = "Project name is required.",
                        Error = new
                        {
                            Code = "INVALID_INPUT",
                            Description = "The project name cannot be null or empty."
                        }
                    });
                }
                string createdBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                // Create project
                bool isCreated = await _projectService.CreateProjectAsync(projectDto, createdBy);

                if (!isCreated)
                {
                    return StatusCode(500, new APIResponse
                    {
                        StatusCode = 500,
                        Success = false,
                        Message = "Failed to create the project.",
                        Error = new
                        {
                            Code = "PROJECT_CREATION_FAILED",
                            Description = "An error occurred while creating the project. Please try again."
                        }
                    });
                }

                return CreatedAtAction(nameof(CreateProject), new APIResponse
                {
                    StatusCode = 201,
                    Success = true,
                    Message = "Project created successfully.",
                    Data = projectDto
                });
            }
           
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new APIResponse
                {
                    StatusCode = 500,
                    Success = false,
                    Message = "Database error occurred",
                    Error = new
                    {
                        Code = "DATABASE_ERROR",
                        Description = ex.InnerException?.Message ?? ex.Message
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse
                {
                    StatusCode = 500,
                    Success = false,
                    Message = "An unexpected error occurred.",
                    Error = new
                    {
                        Code = "INTERNAL_SERVER_ERROR",
                        Description = ex.InnerException.Message ?? ex.Message
                    }
                });
            }
        }


        [Authorize(AuthenticationSchemes = "LoginForLocalUser", Roles = "admin")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("get")]
        public async Task<IActionResult> GetAllProjects(int pageIndex = 1, int pageSize = 5)
        {
            if (pageSize < 1 || pageIndex <= 0)
            {
                return BadRequest(new APIResponse
                {
                    StatusCode = 400,
                    Success = false,
                    Message = "Invalid Request",
                    Error = new
                    {
                        Code = "INVALID_REQUEST",
                        Description = "Page Index and Size are invalid"
                    }
                });
          
            }
            try
            {
                // Fetch all projects from the service
                var projects = await _projectService.GetAllProjectsAsync(pageIndex, pageSize);

                if (projects == null || !projects.Any())
                {
                    return NotFound(new APIResponse
                    {
                        StatusCode = 404,
                        Success = false,
                        Message = "No projects found.",
                        Error = new
                        {
                            Code = "PROJECTS_NOT_FOUND",
                            Description = "No projects were available in the database."
                        }
                    });
                }

                // Build success response
                return Ok(new APIResponse
                {
                    StatusCode = 200,
                    Success = true,
                    Message = "Projects fetched successfully.",
                    Data = projects
                });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new APIResponse
                {
                    StatusCode = 400,
                    Success = false,
                    Message = "Invalid request parameters.",
                    Error = new
                    {
                        Code = "INVALID_REQUEST",
                        Description = ex.Message
                    }
                });
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new APIResponse
                {
                    StatusCode = 500,
                    Success = false,
                    Message = "Database error occurred.",
                    Error = new
                    {
                        Code = "DATABASE_ERROR",
                        Description = ex.InnerException?.Message ?? ex.Message
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse
                {
                    StatusCode = 500,
                    Success = false,
                    Message = "An unexpected error occurred.",
                    Error = new
                    {
                        Code = "INTERNAL_SERVER_ERROR",
                        Description = ex.Message
                    }
                });
            }
        }




        [Authorize(AuthenticationSchemes = "LoginForLocalUser", Roles = "admin, user")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("get/{pid:guid}")]
        public async Task<IActionResult> GetProjectbyId(Guid pid)
        {
            if (pid == Guid.Empty)
            {
                return BadRequest(new APIResponse
                {
                    StatusCode = 400,
                    Success = false,
                    Message = "Project ID cannot be empty.",
                    Error = new { Code = "INVALID_PROJECT_ID", Description = "The provided Project ID is empty or invalid." }
                });
            }

            try
            {
                string Role = User.FindFirst(ClaimTypes.Role)?.Value;
             
                // Fetch the project via service
                var project = await _projectService.GetProjectbyIdAsync(pid, Role);

                // Build and return success response
                return Ok(new APIResponse
                {
                    StatusCode = 200,
                    Success = true,
                    Message = "Project fetched successfully.",
                    Data = project
                });
            }
            catch (ApplicationException ex)
            {
                // Handle application-specific exceptions (e.g., not found)
                return NotFound(new APIResponse
                {
                    StatusCode = 404,
                    Success = false,
                    Message = ex.Message,
                    Error = new { Code = "NOT_FOUND", Description = ex.Message }
                });
            }
            catch (ArgumentException ex)
            {
                // Handle invalid arguments
                return BadRequest(new APIResponse
                {
                    StatusCode = 400,
                    Success = false,
                    Message = ex.Message,
                    Error = new { Code = "INVALID_INPUT", Description = ex.Message }
                });
            }
            catch (InvalidOperationException ex)
            {
                // Handle operational failures
                return StatusCode(500, new APIResponse
                {
                    StatusCode = 500,
                    Success = false,
                    Message = "An unexpected error occurred.",
                    Error = new { Code = "INTERNAL_SERVER_ERROR", Description = ex.InnerException?.Message ?? ex.Message }
                });
            }
        }




        [Authorize(AuthenticationSchemes = "LoginForLocalUser", Roles = "admin, user")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("get/by-uid/")]
        public async Task<IActionResult> GetProjectsbyUserId()
        {
            Guid uid = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (uid == Guid.Empty)
            {
                return BadRequest(new APIResponse
                {
                    StatusCode = 400,
                    Success = false,
                    Message = "User ID cannot be empty.",
                    Error = new
                    {
                        Code = "INVALID_USER_ID",
                        Description = "The provided User ID is empty or invalid."
                    }
                });
            }

            try
            {
                var projects = await _projectService.GetProjectsbyUserIdAsync(uid);

                if (projects == null || !projects.Any())
                {
                    return NotFound(new APIResponse
                    {
                        StatusCode = 404,
                        Success = false,
                        Message = "No projects found for the given user ID.",
                        Error = new
                        {
                            Code = "PROJECTS_NOT_FOUND",
                            Description = "No projects assigned to the specified user."
                        }
                    });
                }

                return Ok(new APIResponse
                {
                    StatusCode = 200,
                    Success = true,
                    Message = "Projects fetched successfully.",
                    Data = projects
                });
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new APIResponse
                {
                    StatusCode = 500,
                    Success = false,
                    Message = "Database error occurred while fetching user projects.",
                    Error = new
                    {
                        Code = "DATABASE_ERROR",
                        Description = ex.InnerException?.Message ?? ex.Message
                    }
                });
            }

            catch (KeyNotFoundException ex)
            {
                return NotFound(new APIResponse
                {
                    StatusCode = 404,
                    Success = false,
                    Message = "No projects found for the given user ID.",
                    Error = new
                    {
                        Code = "PROJECTS_NOT_FOUND",
                        Description = ex.Message
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse
                {
                    StatusCode = 500,
                    Success = false,
                    Message = "An unexpected error occurred.",
                    Error = new
                    {
                        Code = "INTERNAL_SERVER_ERROR",
                        Description = ex.Message
                    }
                });
            }
        }




        [Authorize(AuthenticationSchemes = "LoginForLocalUser", Roles = "admin")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("update/{id:guid}")]
        public async Task<IActionResult> UpdateProject(Guid id, UpdateProjectDto projectDto)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new APIResponse
                {
                    StatusCode = 400,
                    Success = false,
                    Message = "Project ID cannot be empty.",
                    Error = new
                    {
                        Code = "INVALID_PROJECT_ID",
                        Description = "The provided Project ID is invalid."
                    }
                });
            }

            if (projectDto == null)
            {
                return BadRequest(new APIResponse
                {
                    StatusCode = 400,
                    Success = false,
                    Message = "Project data is required.",
                    Error = new
                    {
                        Code = "INVALID_INPUT",
                        Description = "The provided project data is null or invalid."
                    }
                });
            }

            try
            {
                bool isUpdated = await _projectService.UpdateProjectAsync(id, projectDto);

                if (!isUpdated)
                {
                    return NotFound(new APIResponse
                    {
                        StatusCode = 404,
                        Success = false,
                        Message = "Project not found for the given ID.",
                        Error = new
                        {
                            Code = "PROJECT_NOT_FOUND",
                            Description = "The project with the specified ID does not exist."
                        }
                    });
                }

                return Ok(new APIResponse
                {
                    StatusCode = 200,
                    Success = true,
                    Message = "Project updated successfully."
                });
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new APIResponse
                {
                    StatusCode = 500,
                    Success = false,
                    Message = "Database error occurred while updating the project.",
                    Error = new
                    {
                        Code = "DATABASE_ERROR",
                        Description = ex.InnerException?.Message ?? ex.Message
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse
                {
                    StatusCode = 500,
                    Success = false,
                    Message = "An unexpected error occurred.",
                    Error = new
                    {
                        Code = "INTERNAL_SERVER_ERROR",
                        Description = ex.Message
                    }
                });
            }
        }



        [Authorize(AuthenticationSchemes = "LoginForLocalUser", Roles = "admin")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("delete/{id:guid}")]
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            if (id == Guid.Empty)
            {
                // Return bad request for invalid ID
                return BadRequest(new APIResponse
                {
                    StatusCode = 400,
                    Success = false,
                    Message = "Invalid project ID provided.",
                    Error = new
                    {
                        Code = "INVALID_PROJECT_ID",
                        Description = "The provided project ID is empty or invalid."
                    }
                });
            }

            try
            {
                // Call the service to delete the project
                var isDeleted = await _projectService.DeleteProjectAsync(id);

                if (!isDeleted)
                {
                    // Return not found if the project does not exist
                    return NotFound(new APIResponse
                    {
                        StatusCode = 404,
                        Success = false,
                        Message = "Project not found.",
                        Error = new
                        {
                            Code = "NOT_FOUND",
                            Description = "Project with Given Id not found."
                        }
                    });
                }

                // Return success response
                return Ok(new APIResponse
                {
                    StatusCode = 200,
                    Success = true,
                    Message = "Project deleted successfully."
                });
            }
            catch (DbUpdateException ex)
            {
                // Handle database update exceptions
                return StatusCode(500, new APIResponse
                {
                    StatusCode = 500,
                    Success = false,
                    Message = "An error occurred while deleting the project.",
                    Error = new
                    {
                        Code = "DATABASE_ERROR",
                        Description = ex.InnerException?.Message ?? ex.Message
                    }
                });
            }
            catch (Exception ex)
            {
                // Handle unexpected exceptions
                return StatusCode(500, new APIResponse
                {
                    StatusCode = 500,
                    Success = false,
                    Message = "An unexpected error occurred.",
                    Error = new
                    {
                        Code = "INTERNAL_SERVER_ERROR",
                        Description = ex.Message
                    }
                });
            }
        }


        [Authorize(AuthenticationSchemes = "LoginForLocalUser", Roles = "admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("AssignUsers")]
        public async Task<IActionResult> AssignUsers(Guid pid , Guid[] uids)
        {
            if (pid == Guid.Empty || uids.Length < 1)
            {
                return BadRequest(new APIResponse
                {
                    StatusCode = 400,
                    Success = false,
                    Message = "Invalid project ID or Users Id provided.",
                    Error = new
                    {
                        Code = "INVALID_PROJECT_ID",
                        Description = "The provided data is empty or invalid."
                    }
                });

            }
            try
            {
                bool isAssigned = await _userProjectsService.AssignUsersToProjectAsync(pid, uids);

                if (isAssigned)
                {


                    return CreatedAtAction(nameof(AssignUsers), new APIResponse()
                    {
                        StatusCode = 201,
                        Message = "User Assigned successfully",
                        Success = true,
                        Data = isAssigned,
                        Error = null
                    });
                }
                else
                {
                    return Conflict(new APIResponse()
                    {
                        StatusCode = 409,
                        Success = false,
                        Message = "Users already Assigned",
                        Error = new
                        {
                            Code = "CONFLICT",
                            Description = "Users already Assigned"
                        }
                    });
                }
            }
            catch (Exception ex) 
            {

                return StatusCode(500, new APIResponse
                {
                    StatusCode = 500,
                    Success = false,
                    Message = "An unexpected error occurred.",
                    Error = new
                    {
                        Code = "INTERNAL_SERVER_ERROR",
                        Description = ex.Message
                    }
                });

            }
          
            

         
        }


    }
}
