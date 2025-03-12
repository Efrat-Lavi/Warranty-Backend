using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warranty.Core.Interfaces;
using Warranty.Core.Interfaces.Repositories;
using Warranty.Core.Models;

namespace Warranty.Data.Repositories
{
    public class CompanyRepository:Repository<CompanyModel>,ICompanyRepository
    {
        public CompanyRepository(DataContext dataContext) : base(dataContext) { }

        public async Task<List<CompanyModel>> GetFullAsync()
        {
            return await _dbSet.ToListAsync();
        }
    }
}
