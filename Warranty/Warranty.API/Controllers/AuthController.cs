using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Warranty.API.PostModels;
using Warranty.Core.DTOs;
using Warranty.Core.Interfaces.Services;
using Warranty.Service;


namespace Warranty.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService, IMapper mapper) : ControllerBase
    {
        private readonly IAuthService _authService = authService;
        private readonly IMapper _mapper = mapper;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result = await _authService.Login(model.Email, model.Password);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return NotFound(result.ErrorMessage);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserPostModel user)
        {
            if (user == null)
                return BadRequest("User data is required.");

            var registerDto = _mapper.Map<RegisterDto>(user);
            var result = await _authService.Register(registerDto);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }
    }
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
