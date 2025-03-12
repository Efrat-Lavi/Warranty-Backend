using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warranty.API.PostModels;
using Warranty.Core.DTOs;
using Warranty.Core.Interfaces.Services;
using Warranty.Core.Models;
using Warranty.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Warranty.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyServices _iService;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyServices iService, IMapper mapper)
        {
            _iService = iService;
            _mapper = mapper;
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> Get()
        {
            var companies = _iService.GetAllCompanies();
            return companies == null ? NotFound() : Ok(companies);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyDto>> Get(int id)
        {
            var company = await _iService.GetCompanyById(id);
            return company == null ? NotFound() : Ok(company);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<ActionResult<CompanyDto>> Post([FromBody] CompanyPostModel companyPostModel)
        {
            var companyDto = _mapper.Map<CompanyDto>(companyPostModel);
            companyDto = await _iService.AddCompany(companyDto);
            return companyDto == null ? NotFound() : Ok(companyDto);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put(int id, [FromBody] CompanyPostModel companyPostModel)
        {
            var companyDto = _mapper.Map<CompanyDto>(companyPostModel);
            companyDto = await _iService.UpdateCompany(id, companyDto);
            return companyDto == null ? NotFound() : Ok(companyDto);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return await _iService.DeleteCompany(id) ? Ok(true) : NotFound();
        }
    }
}
