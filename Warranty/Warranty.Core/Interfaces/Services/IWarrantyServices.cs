using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warranty.Core.DTOs;
using Warranty.Core.Models;

namespace Warranty.Core.Interfaces.Services
{
    public interface IWarrantyServices
    {
        public Task<IEnumerable<WarrantyDto>> GetAllWarranties();

        public Task<WarrantyDto> GetWarrantyById(int id);

        public Task<WarrantyDto> AddWarranty(WarrantyDto warranty);

        public Task<WarrantyDto> UpdateWarranty(int id, WarrantyDto warranty);

        public Task<bool> DeleteWarranty(int id);
    }
}
