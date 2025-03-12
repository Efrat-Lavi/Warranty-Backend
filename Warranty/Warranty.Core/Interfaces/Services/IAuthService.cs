using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warranty.Core.DTOs;
using Warranty.Core.Models;

namespace Warranty.Core.Interfaces.Services
{
    public interface IAuthService
    {
        string GenerateJwtToken(UserModel user);
        Task<UserModel> ValidateUser(string email, string password);
        Task<Result<LoginResponseDto>> Login(string email, string password);
        Task<Result<bool>> Register(RegisterDto registerDto);
    }
}
