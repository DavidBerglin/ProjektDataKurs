using Microsoft.AspNetCore.Mvc;
using Services;
using Models;
using Models.DTO;

namespace AppWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        private readonly ILogger<AddressController> _logger;

        [HttpGet]
        [ActionName("ReadAddress")]
        [ProducesResponseType(200, Type = typeof(AddressDTO))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> ReadAddressAsync([FromQuery] int number = 100)
        {
            try
            {
                var response = await _addressService.ReadAddressAsync(number);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public AddressController(IAddressService addressService, ILogger<AddressController> logger)
        {
            _addressService = addressService;
            _logger = logger;
        }
    }
}