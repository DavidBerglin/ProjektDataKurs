using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

using Services;
using Configuration;
using Configuration.Options;
using Microsoft.Extensions.Options;
using DbRepos;
using DbModels;
using Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]   
    public class AdminController : Controller
    {
        readonly IAdminService _service;
        readonly ILogger<AdminController> _logger;
        readonly VersionOptions _versionOptions;
    
        [HttpPost()]
        [ActionName("Seed")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> Seed(
            [FromQuery]int number = 100)
        {

            await _service.SeedAsync(number);
            return Ok("Seeding completed successfully");

        }


        [HttpDelete]
        [ActionName("Delete")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> RemoveAsync(bool seeded)
        {
            var result = await _service.RemoveAsync(seeded);
            return Ok(result);
        }

        [HttpDelete]
        [ActionName("Delete SQL expirement")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> RemoveSQL()
        {
            var result = await _service.RemoveSQL();
            return Ok(result);
        }


        [HttpGet()]
        [ActionName("Log")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<LogMessage>))]
        public async Task<IActionResult> Log([FromServices] ILoggerProvider _loggerProvider)
        {
            if (_loggerProvider is InMemoryLoggerProvider cl)
            {
                return Ok(await cl.MessagesAsync);
            }
            return Ok("No messages in log");
        }



        public AdminController(IAdminService service,
            ILogger<AdminController> logger,
            IOptions<VersionOptions> versionOptions)
        {
            _service = service;
            _logger = logger;
            _versionOptions = versionOptions.Value;
        }
    }
}

