using UserServer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserServer.DAL.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByUserNameAsync(string Username);

        Task<List<User>> GetUsersByPageIndexAsync( int pageIndex, int pageSize);
        Task<bool> IsUsernameUniqueAsync(string Username);
        Task<bool> IsUsernameUniqueAsync(string Username, Guid id);
        Task<bool> IsEmailUniqueAsync(string Username);
        Task<bool> IsEmailUniqueAsync(string Email, Guid id);

    }
}
