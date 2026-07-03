using CommerceFabric.Core.DTOs;
using CommerceFabric.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommerceFabric.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        #region Dependencies
        private readonly IUsersService _usersService;
        #endregion

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet("{userID}")]
        public async Task<IActionResult> GetUserByUserID(Guid? userID)
        {
            if(userID == null) return BadRequest("User ID cannot be null");

            var result = await _usersService.GetUserByUserIDAsync(userID);
            if (result == null) return NotFound($"User with ID {userID} not found.");

            return Ok(result);
        }
    }
}
