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
    public class WarrantyRepository : Repository<WarrantyModel>, IWarrantyRepository
    {
        public WarrantyRepository(DataContext dataContext) : base(dataContext) { }
        public async Task<List<WarrantyModel>> GetFull()
        {
            return await _dbSet.Include(w=> w.Company).ToListAsync();
        }
        public async Task<List<WarrantyModel>> GetByIds(List<int> warrantyIds)
        {
            return await _dbSet.Where(w => warrantyIds.Contains(w.Id)).Include(w=>w.Company)
                                 .ToListAsync();
        }

    }
}
