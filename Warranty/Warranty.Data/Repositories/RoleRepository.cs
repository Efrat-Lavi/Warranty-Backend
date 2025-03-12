using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warranty.Core.Interfaces.Repositories;
using Warranty.Core.Models;

namespace Warranty.Data.Repositories
{
    public class RoleRepository : Repository<RoleModel>, IRoleRepository
    {
        public RoleRepository(DataContext dataContext) : base(dataContext) { }
        public async Task<List<RoleModel>> GetFull()
        {
            return await _dbSet.Include(r=> r.Permissions).ToListAsync();
        }
        public async Task<RoleModel> GetRoleByName(string name)
        {
            return await _dbSet.FirstOrDefaultAsync(r => r.NameRole == name);
        }
    }
}
