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
    public class RecordServices : IRecordServices
    {
        private readonly IRepositoryManager _iRepository;
        private readonly IMapper _mapper;

        public RecordServices(IRepositoryManager iRepository, IMapper mapper)
        {
            _iRepository = iRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RecordDto>> GetAllRecords()
        {
            var records = await _iRepository.recordRepository.GetFull();
            return _mapper.Map<IEnumerable<RecordDto>>(records);
        }

        public async Task<RecordDto> GetRecordById(int id)
        {
            var record = await _iRepository.recordRepository.GetById(id);
            return _mapper.Map<RecordDto>(record);
        }
        public async Task<IEnumerable<RecordDto>> GetRecordsByUserId(int userId)
        {
            var records = await _iRepository.recordRepository.GetRecordsByUserId(userId);
            return _mapper.Map<IEnumerable<RecordDto>>(records);
        }
        public async Task<RecordDto> AddRecord(RecordDto recordDto)
        {
            var recordEntity = _mapper.Map<RecordModel>(recordDto);
            if (await _iRepository.warrantyRepository.GetById(recordEntity.WarrantyId) == null)
                return null;
            recordEntity = await _iRepository.recordRepository.Add(recordEntity);
            if (recordEntity != null)
            {
                await _iRepository.Save();
                return _mapper.Map<RecordDto>(recordEntity);
            }
            return null;
        }

        public async Task<RecordDto> UpdateRecord(int id, RecordDto recordDto)
        {
            var recordEntity = _mapper.Map<RecordModel>(recordDto);
            recordEntity = await _iRepository.recordRepository.Update(id, recordEntity);
            if (recordEntity != null)
            {
                await _iRepository.Save();
                return _mapper.Map<RecordDto>(recordEntity);
            }
            return null;
        }

        public async Task<bool> DeleteRecord(int id)
        {
            bool succeed = await _iRepository.recordRepository.Delete(id);
            if (succeed)
                await _iRepository.Save();
            return succeed;
        }

    }

}
