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
        readonly DatabaseConnections _dbConnections;
        readonly IAdminService _service;
        readonly ILogger<AdminController> _logger;
        readonly VersionOptions _versionOptions;

        readonly IAddressService _AddressService;
        readonly AddressDbRepos _AddRepos;
        readonly IAttractionService _Aservice;
        readonly AttractionDbRepos _Arepos;
        readonly IUserService _UserService;
        readonly UserDbRepos _UserRepos;
        readonly CommentDbRepos _CommentRepos;
        readonly ICommentService _CommentService;

        readonly AdminDbRepos _repos;
    
        //GET: api/admin/seed?count={count}
        [HttpPost()]
        [ActionName("Seed")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> Seed([FromQuery]int number)
        {

            await _service.SeedAsync(number);
            return Ok("Seeding completed successfully");

        }


        [HttpPost]
        [ActionName("Read address")]
        [ProducesResponseType(200, Type = typeof(IAddress))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> ReadAddress([FromQuery]int number)
        {
            var respons = await _AddressService.ReadAddressAsync(number);
            return Ok(respons);
        }

        [HttpPost]
        [ActionName("Read comment")]
        [ProducesResponseType(200, Type = typeof(IComments))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> ReadComment([FromQuery]int number)
        {
            var respons = await _CommentService.ReadCommentsAsync(number);
            return Ok(respons);
        }

        [HttpPost("Read user")]
        [ProducesResponseType(200, Type = typeof(IUsers))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> ReadUser([FromQuery] int number)
        {
            var respons = await _UserService.ReadUsersAsync(number);
            return Ok(respons);
        }

        //GET: api/admin/log
        [HttpGet()]
        [ActionName("Log")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<LogMessage>))]
        public async Task<IActionResult> Log([FromServices] ILoggerProvider _loggerProvider)
        {
            //Note the way to get the LoggerProvider, not the logger from Services via DI
            if (_loggerProvider is InMemoryLoggerProvider cl)
            {
                return Ok(await cl.MessagesAsync);
            }
            return Ok("No messages in log");
        }



        public AdminController(IAdminService service,
            ILogger<AdminController> logger,
            DatabaseConnections dbConnections,
            IOptions<VersionOptions> versionOptions,
            AdminDbRepos repos,
            AttractionDbRepos Arepos,
            IAttractionService Aservice,
            IAddressService AddService,
            AddressDbRepos AddRepos,
            UserDbRepos UserRepos,
            IUserService UserService,
            CommentDbRepos CommentRepos,
            ICommentService CommentService)
        {
            _service = service;
            _logger = logger;
            _dbConnections = dbConnections;
            _versionOptions = versionOptions.Value;
            _repos = repos;
            _Arepos = Arepos;
            _Aservice = Aservice;
            _AddressService = AddService;
            _AddRepos = AddRepos;
            _UserRepos = UserRepos;
            _UserService = UserService;
            _CommentRepos = CommentRepos;
            _CommentService = CommentService;
        }
    }
}

