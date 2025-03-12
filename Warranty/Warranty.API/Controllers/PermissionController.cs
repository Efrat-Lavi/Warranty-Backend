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
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionServices _iService;
        private readonly IMapper _mapper;

        public PermissionController(IPermissionServices iService, IMapper mapper)
        {
            _iService = iService;
            _mapper = mapper;
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PermissionDto>>> Get()
        {
            var permissions = await _iService.GetAllPermissions();
            return permissions == null ? NotFound() : Ok(permissions);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("{id}")]
        public async Task<ActionResult<PermissionDto>> Get(int id)
        {
            var permission = await _iService.GetPermissionById(id);
            return permission == null ? NotFound() : Ok(permission);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<ActionResult<PermissionDto>> Post([FromBody] PermissionPostModel permissionPostModel)
        {
            var permissionDto = _mapper.Map<PermissionDto>(permissionPostModel);
            permissionDto = await _iService.AddPermission(permissionDto);
            return permissionDto == null ? NotFound() : Ok(permissionDto);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put(int id, [FromBody] PermissionPostModel permissionPostModel)
        {
            var permissionDto = _mapper.Map<PermissionDto>(permissionPostModel);
            permissionDto = await _iService.UpdatePermission(id, permissionDto);
            return permissionDto == null ? NotFound() : Ok(permissionDto);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return await _iService.DeletePermission(id) ? Ok(true) : NotFound();
        }
    }
}
