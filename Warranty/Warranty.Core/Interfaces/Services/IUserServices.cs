using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warranty.Core.DTOs;
using Warranty.Core.Models;

namespace Warranty.Core.Interfaces.Services
{
    public interface IUserServices
    {
        public Task<IEnumerable<UserDto>> GetAllUsers();

        public Task<UserDto> GetUserById(int id);

        public Task<UserDto> AddUser(UserDto user);

        public Task<UserDto> UpdateUser(int id, UserDto user);

        public Task<bool> DeleteUser(int id);
    }
}
