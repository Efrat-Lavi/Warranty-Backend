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
                           .Include(r => r.Warranty).ToListAsync();
        }

        public async Task<List<RecordModel>> GetRecordsByUserId(int userId)
        {
            //return await _dbSet.Include(r => r.User)
            //               .Include(r => r.Warranty).ThenInclude(w => w.Records)
            //               .Where(r => r.UserId == userId)
            //               .ToListAsync();
            var records = await _dbSet.Include(r => r.User)
                   .Include(r => r.Warranty)
                       .ThenInclude(w => w.Records).ThenInclude(r=>r.User)
                   .Where(r => r.UserId == userId)
                   .ToListAsync();

            foreach (var record in records)
            {
                Console.WriteLine($"Record ID: {record.Id}, Warranty ID: {record.WarrantyId}, Warranty Records Count: {record.Warranty?.Records?.Count}");
            }

            return records;
        }

        public async Task<List<RecordModel>> GetRecordsByWarrantyId(int warrantyId)
        {
            return await _dbSet.Include(r => r.User)
                           .Include(r => r.Warranty)
                           .Where(r => r.WarrantyId == warrantyId)
                           .ToListAsync();
        }
        public async Task<List<RecordModel>> GetRecordsByDate(DateTime date)
        {
            return await _dbSet.Include(r => r.User)
                           .Include(r => r.Warranty)
                           .Where(r => r.Warranty.ExpirationDate == date)
                           .ToListAsync();
        }

    }
}
