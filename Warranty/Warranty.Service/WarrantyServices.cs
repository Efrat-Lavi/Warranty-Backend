using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warranty.Core.DTOs;
using Warranty.Core.Interfaces;
using Warranty.Core.Interfaces.Services;
using Warranty.Core.Models;

namespace Warranty.Service
{
    public class WarrantyServices : IWarrantyServices
    {
        private readonly IRepositoryManager _iRepository;
        private readonly IMapper _mapper;

        public WarrantyServices(IRepositoryManager iRepository, IMapper mapper)
        {
            _iRepository = iRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WarrantyDto>> GetAllWarranties()
        {
            var warranties = await _iRepository.warrantyRepository.GetFull();
            return _mapper.Map<IEnumerable<WarrantyDto>>(warranties);
        }
        public async Task<IEnumerable<WarrantyDto>> GetWarrantiesByUserId(int userId)
        {
            var records = await _iRepository.recordRepository.GetRecordsByUserId(userId);
            var warrantyIds = records.Select(r => r.WarrantyId).ToList();

            var warranties = await _iRepository.warrantyRepository.GetByIds(warrantyIds); 

            return _mapper.Map<IEnumerable<WarrantyDto>>(warranties);
        }


        public async Task<WarrantyDto> GetWarrantyById(int id)
        {
            var warranty = await _iRepository.warrantyRepository.GetById(id);
            return _mapper.Map<WarrantyDto>(warranty);
        }

        public async Task<WarrantyDto> AddWarranty(WarrantyDto warrantyDto)
        {
            var warrantyEntity = _mapper.Map<WarrantyModel>(warrantyDto);
            warrantyEntity = await _iRepository.warrantyRepository.Add(warrantyEntity);
            if (warrantyEntity != null)
            {
                await _iRepository.Save();
                return _mapper.Map<WarrantyDto>(warrantyEntity);
            }
            return null;
        }

        public async Task<WarrantyDto> UpdateWarranty(int id, WarrantyDto warrantyDto)
        {
            var warrantyEntity = _mapper.Map<WarrantyModel>(warrantyDto);
            warrantyEntity = await _iRepository.warrantyRepository.Update(id, warrantyEntity);
            if (warrantyEntity != null)
            {
                await _iRepository.Save();
                return _mapper.Map<WarrantyDto>(warrantyEntity);
            }
            return null;
        }

        public async Task<bool> DeleteWarranty(int id)
        {
            bool succeed = await _iRepository.warrantyRepository.Delete(id);
            if (succeed)
                await _iRepository.Save();
            return succeed;
        }
    }

}
