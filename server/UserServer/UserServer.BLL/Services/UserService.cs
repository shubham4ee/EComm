using AutoMapper;
using UserServer.BLL.DTOs;
using UserServer.DAL.Models;
using UserServer.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;


namespace UserServer.BLL.Services
{
    public class UserService : IUserService
    {
        IUserRepository _userRepository;
        IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper) 
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var users = await _userRepository.GetAllAsync();

            IEnumerable<UserDto> usersDTO = _mapper.Map<IEnumerable<UserDto>>(users);
            return usersDTO;
        }

        public async Task<IEnumerable<UserDto>> GetUsersByPageIndex(int pageIndex, int pageSize)
        {
            var users =  await _userRepository.GetUsersByPageIndexAsync(pageIndex, pageSize);
            IEnumerable<UserDto> usersDTO = _mapper.Map<IEnumerable<UserDto>>(users);
            return usersDTO;
        }

        public async Task<bool> CreateUser(CreateUserDto userDto)
        {
            try
            {
                ValidateUser(userDto.Username, userDto.Email, userDto.Password, userDto.Role);
                bool existingUser = await _userRepository.IsUsernameUniqueAsync(userDto.Username);
                if (existingUser)
                {
                    throw new ValidateUserExceptoins("Username already taken", 409);
                }

                User user = _mapper.Map<User>(userDto);

                await _userRepository.AddAsync(user);
                return true;

            }
            catch(ValidateUserExceptoins ex)
            {
                throw;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> UpdateUserBYid(UpdateUserDto userDto, Guid id)
        {
            try
            {
                ValidateUser(userDto.UserName, userDto.Email, userDto.Password, userDto.Role);
                User user = _mapper.Map<User>(userDto);
                bool isUserNameExist = await _userRepository.IsUsernameUniqueAsync(user.Username, id);
                if (isUserNameExist)
                {
                    throw new ValidateUserExceptoins("Username already taken", 409);
                }
                bool isEmailExist = await _userRepository.IsEmailUniqueAsync(user.Email, id);
                if (isEmailExist)
                {
                    throw new ValidateUserExceptoins("Email already taken", 409);
                }

                user.Id = id;
                user.ModifiedAt = DateTime.UtcNow;

                await _userRepository.UpdateAsync(user);

                return true;

            }
            catch (ValidateUserExceptoins ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void ValidateUser(string username, string email, string password, string role)
        {
            try
            {
                if(string.IsNullOrEmpty(username))
                {
                    throw new ValidateUserExceptoins("Username must be between 5 and 15 characters.", 400);
                }
                var emailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");

                if (string.IsNullOrEmpty(email) || !emailRegex.IsMatch(email))
                {
                    throw new ValidateUserExceptoins("Invalid Email Format",400);
                }
                if (password.Length < 8)
                {
                    throw new ValidateUserExceptoins("Invalid Password Format", 400);
                }

            }
            catch(ValidateUserExceptoins ex)
            {
                throw;
            }

        }
    
        public async Task<User> GetUserByUserName(string Username)
        {
            return await _userRepository.GetUserByUserNameAsync(Username);

        }


        public async Task<bool> DeleteUserById(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            try
            {
                if (user == null)
                {
                    return false; // user not found
                }

                await _userRepository.DeleteAsync(user);
                return true; // user deleted successfully
            }
           

             catch (DbUpdateException ex)
            {
                throw new DbUpdateException("Database error occurred while deleting the User.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error occurred while deleting the User.", ex);
            }
        }
    }


    public class ValidateUserExceptoins : Exception
    {
        public int _statusCode;
        public ValidateUserExceptoins(string message, int statusCode): base(message)
        {
            _statusCode = statusCode;
        }
    
    
    }

}
