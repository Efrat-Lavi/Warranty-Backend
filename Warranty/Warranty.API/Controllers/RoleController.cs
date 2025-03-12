using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
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
    public class RoleController : ControllerBase
    {
        private readonly IRoleServices _iService;
        private readonly IMapper _mapper;

        public RoleController(IRoleServices iService, IMapper mapper)
        {
            _iService = iService;
            _mapper = mapper;
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> Get()
        {
            var roles = await _iService.GetAllRoles();
            return roles == null ? NotFound() : Ok(roles);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDto>> Get(int id)
        {
            var role = await _iService.GetRoleById(id);
            return role == null ? NotFound() : Ok(role);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<ActionResult<RoleDto>> Post([FromBody] RolePostModel rolePostModel)
        {
            var roleDto = _mapper.Map<RoleDto>(rolePostModel);
            roleDto = await _iService.AddRole(roleDto);
            return roleDto == null ? NotFound() : Ok(roleDto);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put(int id, [FromBody] RolePostModel rolePostModel)
        {
            var roleDto = _mapper.Map<RoleDto>(rolePostModel);
            roleDto = await _iService.UpdateRole(id, roleDto);
            return roleDto == null ? NotFound() : Ok(roleDto);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return await _iService.DeleteRole(id) ? Ok(true) : NotFound();
        }
    }
}
