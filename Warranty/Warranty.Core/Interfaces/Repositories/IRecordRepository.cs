using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warranty.Core.Models;

namespace Warranty.Core.Interfaces.Repositories
{
    public interface IRecordRepository:IRepository<RecordModel>
    {
        Task<List<RecordModel>> GetFull();
        Task<List<RecordModel>> GetRecordsByUserId(int userId);
        

    }
}
