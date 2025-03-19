using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warranty.Core.DTOs;
using Warranty.Core.Models;

namespace Warranty.Core.Interfaces.Services
{
    public interface IRoleServices
    {
        public Task<IEnumerable<RoleDto>> GetAllRoles();

        public Task<RoleDto> GetRoleById(int id);

        public Task<RoleDto> AddRole(RoleDto role);
        public Task<RoleDto> UpdateRole(int id, RoleDto role);

        public Task<bool> DeleteRole(int id);
    }
}
