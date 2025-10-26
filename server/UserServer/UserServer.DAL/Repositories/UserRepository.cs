using UserServer.DAL.DataContext;
using UserServer.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserServer.DAL.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<User> _usersDbSet;

        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
            _usersDbSet = _context.Users;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users
                .Where(u => u.Email == email)  // Filter by email
                .Select(u => new User
                {
                    Id = u.Id,
                    Username = u.Username,
                    Password = u.Password,
                    Email = u.Email,
                    Role = u.Role,
                    CreatedAt = u.CreatedAt,
                    ModifiedAt = u.ModifiedAt,
                    Projects = u.UserProjects  // Get UserProjects for the user
                        .Select(up => up.Project)  // Include the Project for each UserProject
                        .ToList()
                })
                .FirstOrDefaultAsync();  // Use FirstOrDefaultAsync to get a single user by email

            return user;
        }

        public async Task<User> GetUserByUserNameAsync(string Username)
        {

            return await _context.Users.FirstOrDefaultAsync(u => u.Username == Username);
        }

        public async Task<List<User>> GetUsersByPageIndexAsync(int pageIndex, int pageSize)
        {
            //return await _context.Users.OrderBy(u => u.CreatedAt).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            var users = await _context.Users
                .OrderBy(u => u.CreatedAt)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .Select(u => new User
                {
                    Id = u.Id,  
                    Username = u.Username,
                    Password = u.Password,
                    Email = u.Email,
                    Role = u.Role,
                    CreatedAt = u.CreatedAt,
                    ModifiedAt = u.ModifiedAt,
                    Projects = u.UserProjects  
                        .Select(up => up.Project) 
                        .ToList()
                })
                .ToListAsync();

            return users;
        }

        public async Task<bool> IsUsernameUniqueAsync(string Username)
        {
            return await _context.Users.AnyAsync(u => u.Username == Username);
        }
        public async Task<bool> IsUsernameUniqueAsync(string Username, Guid id)
        {
            return await _context.Users.AnyAsync(u => u.Username == Username && u.Id != id);
        }
        public async Task<bool> IsEmailUniqueAsync(string Email, Guid id)
        {
            return await _context.Users.AnyAsync(u => u.Email == Email && u.Id != id);
        }
        public async Task<bool> IsEmailUniqueAsync(string Email)
        {
            return await _context.Users.AnyAsync(u => u.Email == Email);
        }




    }
}
