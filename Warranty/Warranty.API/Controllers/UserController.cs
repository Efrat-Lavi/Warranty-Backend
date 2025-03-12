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
    public class UserController : ControllerBase
    {
        private readonly IUserServices _iService;
        private readonly IMapper _mapper;

        public UserController(IUserServices iService, IMapper mapper)
        {
            _iService = iService;
            _mapper = mapper;
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> Get()
        {
            var users = await _iService.GetAllUsers();
            return users == null ? NotFound() : Ok(users);
        }

        [Authorize(Policy = "UserOrAdmin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> Get(int id)
        {
            var user = await _iService.GetUserById(id);
            return user == null ? NotFound() : Ok(user);
        }

        [Authorize(Policy = "UserOrAdmin")]
        [HttpPost]
        public async Task<ActionResult<UserDto>> Post([FromBody] UserPostModel userPostModel)
        {
            var userDto = _mapper.Map<UserDto>(userPostModel);
            userDto = await _iService.AddUser(userDto);
            return userDto == null ? NotFound() : Ok(userDto);
        }

        [Authorize(Policy = "UserOrAdmin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put(int id, [FromBody] UserPostModel userPostModel)
        {
            var userDto = _mapper.Map<UserDto>(userPostModel);
            userDto = await _iService.UpdateUser(id, userDto);
            return userDto == null ? NotFound() : Ok(userDto);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return await _iService.DeleteUser(id) ? Ok(true) : NotFound();
        }
    }
}
