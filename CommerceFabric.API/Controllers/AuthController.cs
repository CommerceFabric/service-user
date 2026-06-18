using CommerceFabric.Core.DTOs;
using CommerceFabric.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommerceFabric.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Dependencies
        private readonly IUsersService _usersService;
        #endregion

        public AuthController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            if(registerRequest == null) throw new Exception("Register request cannot be null");

            var result = await _usersService.Register(registerRequest);

            if (result == null || !result.Success) return Unauthorized(result);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            if(loginRequest == null) throw new Exception("Login request cannot be null");

            var result = await _usersService.Login(loginRequest);

            if (result == null || !result.Success) return BadRequest(result);
            return Ok(result);
        }
    }
}
