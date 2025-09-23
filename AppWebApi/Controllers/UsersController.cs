using Microsoft.AspNetCore.Mvc;
using Services;
using Models;

namespace AppWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        [HttpGet]
        [ActionName("ReadUsersWithComments")]
        [ProducesResponseType(200, Type = typeof(IUsers))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> ReadUsersWithComments([FromQuery] int number = 50)
        {
            try
            {
                var users = await _userService.ReadUsersAsync(number);
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error reading users with comments");
                return BadRequest(ex.Message);
            }
        }

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }
    }
}