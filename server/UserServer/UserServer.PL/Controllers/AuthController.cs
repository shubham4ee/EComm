using AutoMapper;
using UserServer.BLL.Services;
using UserServer.BLL.DTOs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace UserServer.BLL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")] 


    public class AuthController : Controller
    {
        public readonly IConfiguration _configuration;
        public readonly AuthService _authService;
        private readonly APIResponse _apiResponse;
        private readonly IMapper _autoMapper;

        public AuthController(IConfiguration configuration, AuthService authService, IMapper autoMapper)
        {
            _configuration = configuration;
            _authService = authService;
            _apiResponse = new APIResponse();
            _autoMapper = autoMapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> Login(LoginRequestDto model)
        {

            if (User.Identity.IsAuthenticated)
            {
                return Ok(new APIResponse()
                {
                    StatusCode = 200,
                    Success = true,
                    Message = "Already authenticated",
                    Data = User.Identity.Name,
                    Error = null
                });  
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new APIResponse()
                {
                    StatusCode = 400,
                    Success = false,
                    Message = "Invalid Credentials",
                    Error = new
                    {
                        Code = "INVALID_CREDENTIALS",
                        Description = "The provided username or password is incorrect or missing."
                    }
                });
               
            }

            try
            {
                GetUserDto user = await _authService.VerifyUserCredentialAsync(model.Email, model.Password);
                if (user == null)
                {
                    return NotFound(new APIResponse()
                    {
                        StatusCode = 404,
                        Success = false,
                        Message = "User does not exist",
                        Error = new
                        {
                            Code = "USER_NOT_FOUND",
                            Description = "The user does not exist."
                        }
                    });
                 


                }

                var token = _authService.GenerateJWTToken(user);

                var response = new LoginResponseDto();

                response.User = user;
                response.token = token;
                return Created("",new APIResponse()
                {

                    StatusCode = 201,
                    Success = true,
                    Message = "Login successfully",
                    Data = response
                });
           


            }
            catch (Exception ex)
            {       
                var error = new
                {
                    Code = ex.Message switch
                    {
                        "Invalid Email" => "INVALID_EMAIL",
                        "Invalid Password" => "INVALID_PASSWORD",
                        "Incorrect role" => "INCORRECT_ROLE",
                        _ => "INTERNAL_SERVER_ERROR"
                    },
                    Description = ex.Message switch
                    {
                        "Invalid Email" => "The Email provided does not match.",
                        "Invalid Password" => "The password provided is incorrect.",
                        "Incorrect role" => "The provided role does not match the required role for this operation.",
                        _ => "An unexpected error occurred on the server."
                    }
                };
            
                _apiResponse.StatusCode = ex.Message switch
                {
                    "Invalid Email" => 404,
                    "Invalid Password" => 401,
                    "Incorrect role" => 403,
                    _ => 500
                };

                _apiResponse.Success = false;
                _apiResponse.Message = ex.Message;
                _apiResponse.Data = null; 
                _apiResponse.Error = error;
                return ex.Message switch
                {
                    "Invalid Email" => NotFound(_apiResponse),
                    "Invalid Password" => Unauthorized(_apiResponse),
                    "Incorrect role" => StatusCode(403, _apiResponse),
                    _ => StatusCode(500, _apiResponse)

                };
            }
        }
    }
}

