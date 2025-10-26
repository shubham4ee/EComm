using UserServer.BLL.DTOs;
using UserServer.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Minio;
using System;
using System.IO;
using System.Security.Claims;


namespace UserServer.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class CADFilesController : ControllerBase
    {
        public readonly IConfiguration _configuration;
        public readonly IMinioService _minioService;
        public readonly ICADFileService _cADFileService;
        string storagePath;
        public CADFilesController(IConfiguration configuration, IMinioService minioService, ICADFileService cADFileService) 
        {
            _configuration = configuration;
            _minioService = minioService;
            _cADFileService = cADFileService;
            


        }

        #region
        // Generating presing url for file download
        [Authorize(AuthenticationSchemes = "LoginForLocalUser", Roles = "admin")]
        [HttpPost]
        [Route("download")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DownloadCADFiles(string pid, Guid[] fIds)
        {
            if (string.IsNullOrEmpty(pid) || fIds.Length <= 0)
            {
                return BadRequest( new APIResponse() {
                    StatusCode = 400,
                    Success = false,
                    Message = "Invalid Input.",
                    Error = new
                    {
                        Code = "INVALID_INPUT",
                        Description = "The provided  data is null or invalid."
                    }
                } );
            }
            try
            {
                var urls = await _minioService.GetDownloadUrls(pid, fIds);
               
                return Ok(new APIResponse()
                {

                    StatusCode = 200,
                    Success = true,
                    Message = "URL created Successful",
                    Data = urls
                });
   
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new APIResponse()
                {
                    StatusCode = 404,
                    Success = false,
                    Message = "One or more of the input IDs were not found in the database.",
                    Error = new
                    {
                        Code = "INVALID_INPUT",
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
                    Message = "Failed to create the object download url",
                    Error = new
                    {
                        Code = "URL_CREATION_FAILED",
                        Description = "An error occurred while creating the URL. Please try again later."
                    }
                });

            }
            
        }
        #endregion

        // Generating presing url for file upload

        [Authorize(AuthenticationSchemes = "LoginForLocalUser", Roles = "admin")]
        [HttpPost]
        [Route("upload")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UploadCADFiles(Guid pid,  string[] fileNames)
        {
            if (string.IsNullOrEmpty(pid.ToString()) || fileNames.Length <=0)
            {
                return BadRequest(new APIResponse()
                {
                    StatusCode = 400,
                    Success = false,
                    Message = "Invalid Input.",
                    Error = new
                    {
                        Code = "INVALID_INPUT",
                        Description = "The provided  data is null or invalid."
                    }
                });
            }
            try
            {
                Guid uid = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var urls = await _minioService.GetUploadUrls(pid.ToString(), fileNames);
                bool isFileUpload = await _cADFileService.CreateCADFiles(pid, uid,  fileNames);
                if(!isFileUpload)
                {
                    return BadRequest();
                }
                return Ok( new APIResponse()
                {

                    StatusCode = 200,
                    Success = true,
                    Message = "URLs created Successful",
                    Data = urls
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
                        Description = ex.InnerException?.Message ?? ex.Message
                    }
                });

            }

        }


        [Authorize(AuthenticationSchemes = "LoginForLocalUser", Roles = "admin")]
        [HttpDelete]
        [Route("delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCADFile( Guid id)
        {
            if (id == Guid.Empty )
            {
                return BadRequest(new APIResponse()
                {
                    StatusCode = 400,
                    Success = false,
                    Message = "Invalid Input.",
                    Error = new
                    {
                        Code = "INVALID_INPUT",
                        Description = "The provided  data is null or invalid."
                    }
                });
            }
            try
            {
  
                var deletedFile = await _cADFileService.DeleteCADFile(id);
                if(deletedFile == null)
                {
                    return NotFound(new APIResponse
                    {
                        StatusCode = 404,
                        Success = false,
                        Message = "No Files deleted with id = "+id,
                        Error = new
                        {
                            Code = "FILE_NOT_FOUND",
                            Description = "No File found in detabase for this id"
                        }
                    });
                }

                return Ok(new APIResponse()
                {

                    StatusCode = 200,
                    Success = true,
                    Message = "File deleted Successful",
                    Data = deletedFile
                });
            }

            catch (KeyNotFoundException ex)
            {
                return NotFound(new APIResponse
                {
                    StatusCode = 404,
                    Success = false,
                    Message = "No Files deleted with id = " + id,
                    Error = new
                    {
                        Code = "FILE_NOT_FOUND",
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
                        Description = ex.InnerException?.Message ?? ex.Message
                    }
                });

            }
        }


    }
}
