using Microsoft.AspNetCore.Mvc;
using Snippets.Entities;
using Snippets.Interfaces;

namespace Snippets.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;
        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet(Name = "GetCountry")]
        public async Task<ActionResult<List<Root>>> GetCountry()
        {
            return await this._countryService.GetCountry();
        }
    }
}
