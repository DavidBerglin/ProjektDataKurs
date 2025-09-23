using Microsoft.AspNetCore.Mvc;
using Services;
using Models;

namespace AppWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly ILogger<CommentsController> _logger;

        [HttpGet]
        [ActionName("ReadComments")]
        [ProducesResponseType(200, Type = typeof(IComments))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> ReadCommentsAsync([FromQuery] int number = 100)
        {
            try
            {
                await _commentService.ReadCommentsAsync(number);
                return Ok(number);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error reading comments");
                return BadRequest(ex.Message);
            }
        }

        public CommentsController(ICommentService commentService, ILogger<CommentsController> logger)
        {
            _commentService = commentService;
            _logger = logger;
        }
    }
}