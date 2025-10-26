using AutoMapper;
using UserServer.DAL.Models;
using UserServer.BLL.DTOs;

namespace UserServer.BLL.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<User, CreateUserDto>().ReverseMap();
            CreateMap<User, UpdateUserDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, GetUserDto>().ReverseMap();
            CreateMap<Project, GetAdminProjectDto>().ReverseMap();
            CreateMap<Project, CreateProjectDto>().ReverseMap();
            CreateMap<Project, GetUserProjectAllDto>().ReverseMap();
            CreateMap<Project, UpdateProjectDto>().ReverseMap();
            CreateMap<UserProjects, UserProjectsDto>().ReverseMap();
            CreateMap<CreateCADFileDto, CADFile>().ReverseMap();
            CreateMap<GetCADFileDTO, CADFile>().ReverseMap();
            CreateMap<GetUserProjectDto, Project>().ReverseMap();
            CreateMap<GetAllUserDto, User>().ReverseMap();
        }
    }
}
