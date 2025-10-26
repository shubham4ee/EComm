using UserServer.BLL.DTOs;
using UserServer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserServer.BLL.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAll();
        Task<IEnumerable<UserDto>> GetUsersByPageIndex(int pageIndex, int pageSize);
         Task<bool> CreateUser(CreateUserDto user);
        Task<User> GetUserByUserName(string Username);
        Task<bool> UpdateUserBYid(UpdateUserDto user, Guid id);
        Task<bool> DeleteUserById(Guid id);
    }
}
