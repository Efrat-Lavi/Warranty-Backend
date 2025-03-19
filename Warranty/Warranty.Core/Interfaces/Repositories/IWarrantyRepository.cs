using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warranty.Core.Models;

namespace Warranty.Core.Interfaces.Repositories
{
    public interface IWarrantyRepository:IRepository<WarrantyModel>
    {
        Task<List<WarrantyModel>> GetFull();
        Task<List<WarrantyModel>> GetByIds(List<int> warrantyIds);

        

    }
}
