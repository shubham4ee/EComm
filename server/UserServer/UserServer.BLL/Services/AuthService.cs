using AutoMapper;
using UserServer.BLL.DTOs;
using UserServer.DAL.Models;
using UserServer.DAL.Repositories;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace UserServer.BLL.Services
{
    public class AuthService
    {
        IUserRepository _userRepository;
        public readonly IConfiguration _configuration;
        public readonly IMapper _mapper;

        public AuthService(IUserRepository userRepository, IConfiguration configuration, IMapper mapper)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;

        }

        public async Task<GetUserDto> VerifyUserCredentialAsync(string Email, string password)
        {
            // Query the user once from the database.
            var user = await _userRepository.GetUserByEmailAsync(Email);

            if (user == null)
            {
                throw new Exception("Invalid Email");
            }

            if (user.Password != password)
            {
                throw new Exception("Invalid Password");
            }


            var userDTO = _mapper.Map<GetUserDto>(user);

            return userDTO;
        }


        public string GenerateJWTToken(GetUserDto user)
        {
            {
                string audience = string.Empty;
                string issuer = string.Empty;
                byte[] key = null;
                issuer = _configuration.GetValue<string>("LocalIssuer");
                audience = _configuration.GetValue<string>("LocalAudience");
                key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JWTLocalSecret"));

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Issuer = issuer,
                    Audience = audience,
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, user.Role),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),


                    }),
                    Expires = DateTime.Now.AddHours(4),
                    SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)

                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                string response = tokenHandler.WriteToken(token);
                return response;
            }


        }
    }
}
