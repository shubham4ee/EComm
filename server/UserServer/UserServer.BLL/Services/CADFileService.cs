using AutoMapper;
using UserServer.BLL.DTOs;
using UserServer.DAL.Models;
using UserServer.DAL.Repositories;
using Minio.DataModel.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserServer.BLL.Services
{
    public class CADFileService : ICADFileService
    {
        public readonly ICADFileRepository _CadFileRepository;
        public readonly IMapper _autoMapper;
        public CADFileService(ICADFileRepository CadFileRepository, IMapper autoMapper) 
        {
            _CadFileRepository = CadFileRepository;
            _autoMapper = autoMapper;
        }

        public async Task<bool> CreateCADFiles(Guid pid, Guid uid, string[] fileNames)
        {
            try
            {
                List<CreateCADFileDto> cadFilesDto = new List<CreateCADFileDto>();
                foreach (string fileName in fileNames)
                {
                    CreateCADFileDto fileDto = new CreateCADFileDto();
                    fileDto.ProjectId = pid;
                    fileDto.FileName = fileName;
                    fileDto.FilePath = pid + "/" + fileName;
                    fileDto.UploadedBy = uid;

                    cadFilesDto.Add(fileDto);
                }

                List<CADFile> cADFiles = _autoMapper.Map<List<CADFile>>(cadFilesDto);
                await _CadFileRepository.AddRangeAsync(cADFiles);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            

        }

        public async Task<GetCADFileDTO> DeleteCADFile(Guid fid)
        {
            try
            {

                CADFile existingFile = await _CadFileRepository.GetByIdAsync(fid);
                if (existingFile == null)
                {
                    throw new KeyNotFoundException("No File found for the given File ID.");
                }
                await _CadFileRepository.DeleteAsync(existingFile);
                GetCADFileDTO deletedFile = _autoMapper.Map<GetCADFileDTO>(existingFile);
                return deletedFile;
            }
            catch (KeyNotFoundException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
