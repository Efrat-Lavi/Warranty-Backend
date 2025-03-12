using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warranty.Core.DTOs;
using Warranty.Core.Models;

namespace Warranty.Core.Interfaces.Services
{
    public interface ICompanyServices
    {


        Task<IEnumerable<CompanyDto>> GetAllCompanies();

        Task<CompanyDto> GetCompanyById(int id);

        Task<CompanyDto> AddCompany(CompanyDto company);

        Task<CompanyDto> UpdateCompany(int id, CompanyDto company);

        Task<bool> DeleteCompany(int id);
    }
}
