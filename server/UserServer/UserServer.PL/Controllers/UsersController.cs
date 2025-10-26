using AutoMapper;
using UserServer.BLL.Services;
using UserServer.BLL.DTOs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using UserServer.DAL.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace UserServer.BLL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly APIResponse _apiResponse;
        private readonly IMapper _automapper;
        private readonly IUserProjectsService _userProjectsService;
        public UsersController(IUserService userService, IMapper automapper, IUserProjectsService userProjectsService)
        {
            _userService = userService;
            _automapper = automapper;
            _apiResponse = new APIResponse();
            _userProjectsService = userProjectsService;
        }

        [Authorize(AuthenticationSchemes = "LoginForLocalUser", Roles = "admin")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetUsers(int pageIndex = 1, int pageSize = 5)
        {
            if (pageSize < 1 || pageIndex <= 0)
            {

                return BadRequest(new APIResponse
                {
                    StatusCode = 404,
                    Success = false,
                    Message = "Invalid Request",
                    Data = null,
                    Error = new
                    {
                        Code = "INVALID_REQUEST",
                        Description = "Page Index and Size are invalid"

                    }
                });
            }

            var users = await _userService.GetUsersByPageIndex(pageIndex, pageSize);
            if (users.IsNullOrEmpty())
            {
                
                return NotFound(new APIResponse
                {
                    StatusCode = 404,
                    Success = false,
                    Message = "Users Does not exist",
                    Data = null,
                    Error = new
                    {
                        Code = "DATA_NOT_FOUND",
                        Description = "Users Does not exist"

                    }
                });
            }

           
            return Ok(new APIResponse
            {
                StatusCode = 200,
                Success = true,
                Message = "Users Fetch successfully",
                Data = users,
                Error = null
            });
        }


        [Authorize(AuthenticationSchemes = "LoginForLocalUser", Roles = "admin")]
        [HttpPost]
        [Route("CreateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateUser( [FromBody] CreateUserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new APIResponse
                {
                    StatusCode = 400,
                    Success = false,
                    Message = "User Credentials Missing",
                    Data = null,
                    Error = new
                    {
                        Code = "INVALID_CREDENTIALS",
                        Description = "The provided Credentials are incorrect or missing."
                    }
                });



            }
            try
            {
                await _userService.CreateUser(userDto);
                return Ok(new APIResponse
                {
                    StatusCode = 200,
                    Success = true,
                    Message = "Users Created successfully",
                    Data = null,
                    Error = null
                });



            }
            catch( ValidateUserExceptoins ex)
            {
                if (ex._statusCode == 400)
                {
                    
                    return BadRequest(new APIResponse
                    {
                        StatusCode = 400,
                        Success = false,
                        Message = "Invalid Format",
                        Data = null,
                        Error = new
                        {
                            Code = "INVALID_FORMAT",
                            Description = ex.Message
                        }
                    });


                }
                if (ex._statusCode == 409)
                {
                   
                    return Conflict(new APIResponse
                    {
                        StatusCode = 409,
                        Success = false,
                        Message = "Username Conflict",
                        Data = null,
                        Error = new
                        {
                            Code = "USERNAME_CONFLICT",
                            Description = ex.Message
                        }
                    });
                }
                throw new Exception(ex.Message);
            }
            catch(Exception ex)
            {
              
                return StatusCode(500, new APIResponse
                {
                    StatusCode = 500,
                    Success = false,
                    Message = ex.Message,
                    Data = null,
                    Error = new
                    {
                        Code = "INTERNAL_SERVER_ERROR",
                        Description = "An unexpected error occurred on the server."
                    }
                });

            }
        }

        [Authorize(AuthenticationSchemes = "LoginForLocalUser", Roles = "admin")]
        [HttpPut]
        [Route("UpdateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto userDto, Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new APIResponse
                {
                    StatusCode = 400,
                    Success = false,
                    Message = "User Credentials Missing",
                    Data = null,
                    Error = new
                    {
                        Code = "INVALID_CREDENTIALS",
                        Description = "The provided Credentials are incorrect or missing."
                    }
                });

            }

            if (id == Guid.Empty || userDto == null)
            {
                return BadRequest(new APIResponse
                {
                    StatusCode = 400,
                    Success = false,
                    Message = "Incorrect or missing Parameters",
                    Data = null,
                    Error = new
                    {
                        Code = "INVALID_REQUEST",
                        Description = "The provided Paramters are incorrect or missing."
                    }
                });
            }

            try
            { 
                await _userService.UpdateUserBYid(userDto, id);

                return Ok(new APIResponse
                {
                    StatusCode = 200,
                    Success = true,
                    Message = "User Update Successful",
                    Data = userDto,
                    Error = null
                });
            }
            catch (ValidateUserExceptoins ex)
            {
                if (ex._statusCode == 400)
                {
                    return BadRequest(new APIResponse
                    {
                        StatusCode = 400,
                        Success = false,
                        Message = "Invalid Format",
                        Data = null,
                        Error = new
                        {
                            Code = "INVALID_FORMAT",
                            Description = ex.InnerException?.Message ?? ex.Message
                        }
                    });

                }
                if (ex._statusCode == 409)
                {
                    return Conflict(new APIResponse
                    {
                        StatusCode = 409,
                        Success = false,
                        Message = ex.Message,
                        Data = null,
                        Error = new
                        {
                            Code = "CONFLICT",
                            Description = ex.InnerException?.Message ?? ex.Message
                        }
                    });
                }
                throw new Exception(ex.Message);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new APIResponse
                {
                    StatusCode = 500,
                    Success = false,
                    Message = "An error occurred while accessing the database.",
                    Data = null,
                    Error = new
                    {
                        Code = "DATABASE_ERROR",
                        Description = ex.InnerException?.Message ?? ex.Message
                    }
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
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse
                {
                    StatusCode = 500,
                    Success = false,
                    Message = "An unexpected error occurred.",
                    Data = null,
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
        [Route("DeleteUser")]
        public async Task<IActionResult> DeleteUserById(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new APIResponse
                {
                    StatusCode = 400,
                    Success = false,
                    Message = "User Credentials Missing",
                    Data = null,
                    Error = new
                    {
                        Code = "INVALID_CREDENTIALS",
                        Description = "The provided Credentials are incorrect or missing."
                    }
                });
            }

            if (id == Guid.Empty )
            {
                return BadRequest(new APIResponse
                {
                    StatusCode = 400,
                    Success = false,
                    Message = "Incorrect or missing Parameters",
                    Data = null,
                    Error = new
                    {
                        Code = "INVALID_REQUEST",
                        Description = "The provided Paramters are incorrect or missing."
                    }
                });
            }

            try
            {
                if(await _userService.DeleteUserById(id))
                {
                    return Ok(new APIResponse
                    {
                        StatusCode = 200,
                        Success = true,
                        Message = "User Deleted Successful, having userid as : " + id,
                        Data = id,
                        Error = null
                    });

                }
                else
                {
                    return NotFound(new APIResponse
                    {
                        StatusCode = 404,
                        Success = false,
                        Message = "User Not Found cannot delete",
                        Error = "NOT_FOUND"
                    });
                }
           
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new APIResponse
                {
                    StatusCode = 500,
                    Success = false,
                    Message = "An error occurred while accessing the database.",
                    Data = null,
                    Error = new
                    {
                        Code = "DATABASE_ERROR",
                        Description = ex.InnerException?.Message ?? ex.Message
                    }
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
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse
                {
                    StatusCode = 500,
                    Success = false,
                    Message = "An unexpected error occurred.",
                    Data = null,
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
        [Route("AssignProjects")]
        public async Task<IActionResult> AssignProjects(Guid uid, Guid[] pids)
        {
            if (uid == Guid.Empty || pids.Length < 1)
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
                bool isAssigned = await _userProjectsService.AssignProjectsToUserAsync(uid, pids);

                if (isAssigned)
                {


                    return CreatedAtAction(nameof(AssignProjects), new APIResponse()
                    {
                        StatusCode = 201,
                        Message = "Projects Assigned successfully",
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
                        Message = "Projects already Assigned",
                        Error = new
                        {
                            Code = "CONFLICT",
                            Description = "Projects already Assigned"
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
