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
using Models.DTO;
using System.ComponentModel;
using System.Xml;


namespace AppWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AttractionController : Controller
    {
        readonly IAttractionService _Aservice;


        [HttpGet]
        [ActionName("Read attractions")]
        [ProducesResponseType(200, Type = typeof(ReadAttractionSummaryDTO))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> ReadAttractionALL(
            [FromQuery][DefaultValue(10)] int number,
            [Description("If true, includes comments")] bool comment = false)

        {
            try
            {
                var respons = await _Aservice.ReadAttraction(number, comment);
                return Ok(respons);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [HttpGet]
        [ActionName("Search")]
        [ProducesResponseType(200, Type = typeof(ReadAttractionSummaryDTO))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> ReadAttractionFilter(bool seeded, string filter)
        {
            try
            {
                var respons = await _Aservice.ReadAttractionFilter(seeded, filter);
                return Ok(respons);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
          
        }

        public AttractionController(IAttractionService Aservice)
        {
            _Aservice = Aservice;

        }
    }
    
 
} 