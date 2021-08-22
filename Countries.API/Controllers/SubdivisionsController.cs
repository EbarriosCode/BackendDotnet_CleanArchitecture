using Application.Interfaces;
using Countries.Common.Classes;
using Countries.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Countries.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubdivisionsController : ControllerBase
    {
        private readonly ISubdivisionsService _service;

        public SubdivisionsController(ISubdivisionsService service) => this._service = service;

        // GET: api/Subdivisions
        [HttpGet]
        public async Task<IEnumerable<Subdivision>> GetCountries([FromQuery] SubdivisionParameter parameters)
        {
            if (parameters == null)
                return null;

            var subdivisions = await this._service.GetSubdivisions(parameters);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(subdivisions.MetaData));

            return subdivisions;
        }

        // GET: api/Subdivisions/5
        [HttpGet("{SubdivisionID}")]
        public async Task<ActionResult<Subdivision>> GetSubdivision(int SubdivisionID)
        {
            if (SubdivisionID <= 0)
                return BadRequest("Invalid ID");

            var subdivision = await Task.Run(() => this._service.GetSubdivision(SubdivisionID));

            if (subdivision == null)
                return NotFound();

            return subdivision;
        }

        // POST: api/Subdivisions
        [HttpPost]
        public async Task<ActionResult<Subdivision>> PostSubdivision([FromBody] Subdivision subdivision)
        {
            if (subdivision == null)
                return BadRequest("Error when trying to create the country");

            await this._service.CreateSubdivisionAsync(subdivision);

            return StatusCode((int)HttpStatusCode.Created, subdivision);
        }

        // PUT: api/Subdivisions        
        [HttpPut]
        public async Task<IActionResult> PutSubdivision([FromBody] Subdivision subdivision)
        {
            if (subdivision == null)
                return BadRequest();

            var updated = await this._service.UpdateSubdivisionAsync(subdivision);
            return Ok(updated);
        }

        // DELETE: api/Subdivisions/5
        [HttpDelete("{SubdivisionID}")]
        public async Task<IActionResult> DeleteSubdivision(int SubdivisionID)
        {
            if (SubdivisionID <= 0)
                return BadRequest("Invalid ID");

            var subdivision = this._service.GetSubdivision(SubdivisionID);

            if (subdivision == null)
                return NotFound();

            await this._service.DeleteSubdivisionAsync(subdivision);
            return NoContent();
        }
    }
}