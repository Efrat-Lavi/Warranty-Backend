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
    public class RecordRepository : Repository<RecordModel>, IRecordRepository
    {
        public RecordRepository(DataContext dataContext) : base(dataContext) { }

        public async Task<List<RecordModel>> GetFull()
        {
            return await _dbSet.Include(r => r.User)
                           .Include(r => r.Warranty)
                           .Include(r=>r.RoleWarranty)
                            .ToListAsync();
        }

        public async Task<List<RecordModel>> GetRecordsByUserId(int userId)
        {
            return await _dbSet.Include(r => r.User)
                           .Include(r => r.Warranty)
                           .Where(r => r.UserId == userId)
                           .ToListAsync();
        }

    }
}
