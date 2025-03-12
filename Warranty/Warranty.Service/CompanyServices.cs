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
    public class CompanyServices:ICompanyServices
    {
        private readonly IRepositoryManager _iRepository;
        private readonly IMapper _mapper;
        public CompanyServices(IRepositoryManager iRepository, IMapper mapper)
        {
            _iRepository = iRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CompanyDto>> GetAllCompanies()
        {
            var comanies = await _iRepository.companyRepository.GetAll();
            var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(comanies);
            return companiesDto;
        }

        public async Task<CompanyDto> GetCompanyById(int id)
        {
            var company = await _iRepository.companyRepository.GetById(id);
            return _mapper.Map<CompanyDto>(company);
        }

        public async Task<CompanyDto> AddCompany(CompanyDto company)
        {
            var newCompany = _mapper.Map<CompanyModel>(company);

            newCompany = await _iRepository.companyRepository.Add(newCompany);
            if (newCompany != null)
            {
                await _iRepository.Save();
                return _mapper.Map<CompanyDto>(newCompany);
            }
            return null;
        }

        public async Task<CompanyDto> UpdateCompany(int id, CompanyDto company)
        {
            var updatedCompany = _mapper.Map<CompanyModel>(company);
            updatedCompany = await _iRepository.companyRepository.Update(id, updatedCompany);
            if (updatedCompany != null)
            {
                await _iRepository.Save();
                return _mapper.Map<CompanyDto>(updatedCompany);
            }
            return null;
        }

        public async Task<bool> DeleteCompany(int id)
        {
            bool succeed = await _iRepository.companyRepository.Delete(id);
            if (succeed)
                await_iRepository.Save();
            return succeed;
        }
    }
}
