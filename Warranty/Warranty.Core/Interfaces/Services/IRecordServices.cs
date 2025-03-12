using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warranty.Core.DTOs;
using Warranty.Core.Models;

namespace Warranty.Core.Interfaces.Services
{
    public interface IRecordServices
    {
        public Task<IEnumerable<RecordDto>> GetAllRecords();

        public Task<RecordDto> GetRecordById(int id);

        public Task<RecordDto> AddRecord(RecordDto record);

        public Task<RecordDto> UpdateRecord(int id, RecordDto record);

        public Task<bool> DeleteRecord(int id);
    }
}
