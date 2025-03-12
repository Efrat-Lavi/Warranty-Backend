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
    public class WarrantyController : ControllerBase
    {
        private readonly IWarrantyServices _iService;
        private readonly IMapper _mapper;

        public WarrantyController(IWarrantyServices iService, IMapper mapper)
        {
            _iService = iService;
            _mapper = mapper;
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WarrantyDto>>> Get()
        {
            var warranties = await _iService.GetAllWarranties();
            return warranties == null ? NotFound() : Ok(warranties);
        }

        [Authorize(Policy = "UserOrAdmin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<WarrantyDto>> Get(int id)
        {
            var warranty = await _iService.GetWarrantyById(id);
            return warranty == null ? NotFound() : Ok(warranty);
        }

        [Authorize(Policy = "UserOrAdmin")]
        [HttpPost]
        public async Task<ActionResult<WarrantyDto>> Post([FromBody] WarrantyPostModel warrantyPostModel)
        {
            var warrantyDto = _mapper.Map<WarrantyDto>(warrantyPostModel);
            warrantyDto = await _iService.AddWarranty(warrantyDto);
            return warrantyDto == null ? NotFound() : Ok(warrantyDto);
        }

        [Authorize(Policy = "UserOrAdmin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put(int id, [FromBody] WarrantyPostModel warrantyPostModel)
        {
            var warrantyDto = _mapper.Map<WarrantyDto>(warrantyPostModel);
            warrantyDto = await _iService.UpdateWarranty(id, warrantyDto);
            return warrantyDto == null ? NotFound() : Ok(warrantyDto);
        }

        [Authorize(Policy = "UserOrAdmin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return await _iService.DeleteWarranty(id) ? Ok(true) : NotFound();
        }
    }
}
