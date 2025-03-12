using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warranty.Core.DTOs;
using Warranty.Core.Models;

namespace Warranty.Core.Interfaces.Services
{
    public interface IPermissionServices
    {
        public Task<IEnumerable<PermissionDto>> GetAllPermissions();

        public Task<PermissionDto> GetPermissionById(int id);

        public Task<PermissionDto> AddPermission(PermissionDto permission);

        public Task<PermissionDto> UpdatePermission(int id, PermissionDto permission);

        public Task<bool> DeletePermission(int id);
    }
}
