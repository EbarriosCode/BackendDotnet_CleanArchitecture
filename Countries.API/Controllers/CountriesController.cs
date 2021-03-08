using Application.Helpers;
using Application.Interfaces;
using Countries.Common.Classes;
using Countries.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Countries.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountriesService _service;

        public CountriesController(ICountriesService service) => this._service = service;

        // GET: api/Countries
        [HttpGet]
        public async Task<IEnumerable<Country>> GetCountries([FromQuery] CountryParameters parameters)
        {
            if (parameters == null)
                return null;

            var countries = await this._service.GetCountries(parameters);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(countries.MetaData));

            return countries;
        }

        // GET: api/Countries/5
        [HttpGet("{CountryID}")]
        public async Task<ActionResult<Country>> GetCountry(int CountryID)
        {
            if (CountryID <= 0)
                return BadRequest("Invalid ID");

            var country = await Task.Run(() => this._service.GetCountry(CountryID));

            if (country == null)
                return NotFound();            

            return country;
        }

        // POST: api/Countries
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry([FromBody] Country country)
        {
            if (country == null)
                return BadRequest("Error when trying to create the country");

            await this._service.CreateCountryAsync(country);

            return StatusCode((int)HttpStatusCode.Created, country);
        }

        // PUT: api/Countries        
        [HttpPut]
        public async Task<IActionResult> PutCountry([FromBody] Country country)
        {
            if (country == null)
                return BadRequest();

            var updated = await this._service.UpdateCountryAsync(country);
            return Ok(updated);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{CountryID}")]
        public async Task<IActionResult> DeleteCountry(int CountryID)
        {
            if (CountryID <= 0)
                return BadRequest("Invalid ID");

            var country = this._service.GetCountry(CountryID);

            if (country == null)
                return NotFound();

            await this._service.DeleteCountryAsync(country);
            return NoContent();
        }
    }
}
