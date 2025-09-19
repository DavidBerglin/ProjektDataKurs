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


namespace AppWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AttractionController : Controller
    {
        readonly IAttractionService _Aservice;



        [HttpPost]
        [ActionName("Read attractionDTO")]
        [ProducesResponseType(200, Type = typeof(IAttraction))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> ReadAttraction([FromQuery] string id, string flat)
        {
            try
            {
                var idArg = Guid.Parse(id);
                bool flatArg = bool.Parse(flat);

                var item = await _Aservice.ReadAttractionsAsync(idArg, flatArg);
                if (item == null) throw new ArgumentException($"Couldnt find attraction with id: {id}");
                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Read attraction")]
        [ProducesResponseType(200, Type = typeof(IAttraction))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> ReadAttractionALL([FromQuery] int number)
        {
            var respons = await _Aservice.ReadAttraction(number);
            return Ok(respons);
        }
    
        [HttpPost("Attractions without comment")]
        [ProducesResponseType(200, Type = typeof(IAttraction))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> ReadAttractionNoComment()
        {
            var respons = await _Aservice.ReadAttractionNoComment();
            return Ok(respons);
        }

        public AttractionController(IAttractionService Aservice)
        {
            _Aservice = Aservice;

        }
    }
    
 
} 