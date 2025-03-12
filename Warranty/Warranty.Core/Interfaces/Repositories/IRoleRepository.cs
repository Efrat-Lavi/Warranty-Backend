using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warranty.Core.Models;

namespace Warranty.Core.Interfaces.Repositories
{
    public interface IRoleRepository:IRepository<RoleModel>
    {
        Task<List<RoleModel>> GetFull();
        Task<RoleModel> GetRoleByName(string name);

    }
}
