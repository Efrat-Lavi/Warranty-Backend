using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
            return await _dbSet.Include(w=> w.Company).Include(w=>w.Records).ToListAsync();
        }
        public async Task<List<WarrantyModel>> GetByIds(List<int> warrantyIds)
        {
            return await _dbSet.Where(w => warrantyIds.Contains(w.Id)).Include(w=>w.Company).Include(w => w.Records)
                                 .ToListAsync();
        }
        //public async Task<WarrantyModel> UpdateWarranty(int id, WarrantyModel warranty)
        //{
        //    var w = new WarrantyModel
        //    {
        //        Id = id,
        //        Company = warranty.Company,
        //        users = warranty.users,
        //        LinkFile = warranty.LinkFile,
        //        NameProduct = warranty.NameProduct,
        //        ExpirationDate = warranty.ExpirationDate
        //    };
        //     _dbSet.Update( w);
        //    return w;
        //}
        //public async Task<WarrantyModel> UpdateWarranty(int id, WarrantyModel warranty)
        //{
        //    var existingWarranty = await _dbSet.FindAsync(id);
        //    if (existingWarranty == null)
        //    {
        //        throw new KeyNotFoundException($"Warranty with ID {id} not found");
        //    }

        //    // מעדכנים רק אם הערך אינו null או 0


        //    if (warranty.Users != null && warranty.Users.Count > 0)
        //        existingWarranty.Users = warranty.Users;

        //    if (!string.IsNullOrEmpty(warranty.LinkFile))
        //        existingWarranty.LinkFile = warranty.LinkFile;

        //    if (!string.IsNullOrEmpty(warranty.NameProduct))
        //        existingWarranty.NameProduct = warranty.NameProduct;

        //    if (warranty.ExpirationDate != DateTime.MinValue)
        //        existingWarranty.ExpirationDate = warranty.ExpirationDate;

        //     _dbSet.Update(existingWarranty);

        //    return existingWarranty;
        //}

    }
}
