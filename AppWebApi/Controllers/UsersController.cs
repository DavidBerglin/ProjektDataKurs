using Microsoft.AspNetCore.Mvc;
using Services;
using Models;
using Models.DTO;

namespace AppWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        [HttpGet]
        [ActionName("Users and comments")]
        [ProducesResponseType(200, Type = typeof(ReadUsersCommentsDTO))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> ReadUsersWithComments(int number = 100)
        {
            try
            {
                var respons = await _userService.ReadUsersAsync(number);
                return Ok(respons);
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