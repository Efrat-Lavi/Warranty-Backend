using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Warranty.Core.DTOs;
using Warranty.Core.Interfaces.Services;
using Warranty.Core.Interfaces;
using Warranty.Core.Models;
using Warranty.Core;
using System.Data;
using System.Threading.Tasks;
namespace Warranty.Service
{
    public class AuthService(IConfiguration _configuration, IRepositoryManager _repositoryManager) : IAuthService
    {
       
        public string GenerateJwtToken(UserModel user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.NameUser),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };


            claims.Add(new Claim(ClaimTypes.Role, user.Role.NameRole));

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<UserModel> ValidateUser(string email, string password)
        {
            UserModel user = await _repositoryManager.userRepository.GetUserByEmail(email);
            Console.WriteLine(user.RoleId);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.HashPassword))
            {
                return user;
            }

            return null;
        }


        public async Task<Result<LoginResponseDto>> Login(string email, string password)
        {
            UserModel user = await ValidateUser(email, password);
            if (user != null)
            {
                var token = GenerateJwtToken(user);
                var userDto = new UserDto
                {
                    Id = user.Id,
                    NameUser = user.NameUser,
                    Email = user.Email,
                    Role = user.Role.NameRole
                };
                var response = new LoginResponseDto
                {
                    User = userDto,
                    Token = token
                };
                return Result<LoginResponseDto>.Success(response);
            }

            return Result<LoginResponseDto>.Failure("Invalid username or password.");
        }

        public async Task<Result<bool>> Register(RegisterDto registerDto)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
            var role = await _repositoryManager.roleRepository.GetRoleByName(registerDto.Role);
           
            var user = new UserModel
            {
                NameUser = registerDto.NameUser,
                Email = registerDto.Email,
                HashPassword = hashedPassword,
                Role = role
            };

            if (await _repositoryManager.userRepository.GetUserByEmail(user.Email) != null)
            {
                return Result<bool>.Failure("Email already exists");
            }

            var result = _repositoryManager.userRepository.Add(user);
            if (result == null)
            {
                return Result<bool>.Failure("Failed to register user.");
            }

            await _repositoryManager.Save();
            return Result<bool>.Success(true);
        }
    }
}
