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
    public class PermissionRepository : Repository<PermissionModel>, IPermissionRepository
    {
        public PermissionRepository(DataContext dataContext) : base(dataContext) { }

        public async Task<List<PermissionModel>> GetFull()
        {
            return  await _dbSet.ToListAsync();
        }
    }
}
