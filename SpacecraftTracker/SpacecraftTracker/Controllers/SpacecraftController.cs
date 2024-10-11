using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SpacecraftTracker.Domain.Entities;
using SpacecraftTracker.Service.Interfaces;
using SpacecraftTracker.WebAPI.CustomActionFilters;
using SpacecraftTracker.WebAPI.DTO;
using System.Net;
using System.Text.Json;

namespace SpacecraftTracker.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpacecraftController : ControllerBase
    {
        private readonly ISpacecraftService spacecraftService;
        private readonly IMapper mapper;

        public SpacecraftController(ISpacecraftService spacecraftService, IMapper mapper)
        {
            this.spacecraftService = spacecraftService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var spacecraftDomain = await spacecraftService.GetAllSpacecraftsAsync();

            return Ok(mapper.Map<List<GetAllSpacecraftsDTO>>(spacecraftDomain));
        }

        [HttpPost]
        [ValidateCallsign]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody]CreateSpacecraftDTO createSpacecraft)
        {
            var spacecraftDomain = mapper.Map<Spacecraft>(createSpacecraft);

            await spacecraftService.CreateSpacecraftAsync(spacecraftDomain);

            var spacecraftDTO = mapper.Map<SpacecraftDTO>(spacecraftDomain);

            return CreatedAtAction(nameof(Get), new { id = spacecraftDTO.Id }, spacecraftDTO);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var spacecraftDomain = await spacecraftService.GetSpacecraftAsync(id);

            if (spacecraftDomain == null)
                return NotFound();

            return Ok(mapper.Map<SpacecraftDTO>(spacecraftDomain));
        }

        [HttpPatch]
        [ValidateUpdateCallsign]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] SpacecraftDTO updateSpacecraft)
        {
            var spacecraftDomain = mapper.Map<Spacecraft>(updateSpacecraft);

            spacecraftDomain = await spacecraftService.UpdateSpacecraftAsync(spacecraftDomain);

            if (spacecraftDomain == null)
                return NotFound();

            return Ok(mapper.Map<SpacecraftDTO>(spacecraftDomain));
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var spacecraftDomain = await spacecraftService.DeleteSpacecraftAsync(id);

            if (spacecraftDomain == null)
                return NotFound();

            // adatbázisban cascade DELETEnél, nincs időm javítani
            //if (spacecraftDomain.Flights.Count != 0)
            //{
            //    return Conflict();
            //}

            return Ok();
        }
    }
}
