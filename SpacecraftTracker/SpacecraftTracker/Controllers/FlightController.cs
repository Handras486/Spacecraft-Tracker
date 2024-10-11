using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SpacecraftTracker.Domain.Entities;
using SpacecraftTracker.Service;
using SpacecraftTracker.Service.Interfaces;
using SpacecraftTracker.WebAPI.CustomActionFilters;
using SpacecraftTracker.WebAPI.DTO;

namespace SpacecraftTracker.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService flightService;
        private readonly IMapper mapper;

        public FlightController(IFlightService flightService, IMapper mapper)
        {
            this.flightService = flightService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var flightDomain = await flightService.GetAllFlightsAsync();

            return Ok(mapper.Map<List<GetAllFlightsDTO>>(flightDomain));
        }

        [HttpPost]
        [ValidateCreateFlight]
        [Authorize(Roles = "Admin, Carrier")]
        public async Task<IActionResult> Create([FromBody] FlightDTO addFlight)
        {
            var flightDomain = mapper.Map<Flight>(addFlight);

            await flightService.CreateFlightAsync(flightDomain);

            var flightDTO = mapper.Map<GetFlightDTO>(flightDomain);

            return CreatedAtAction(nameof(Get), new { id = flightDTO.Id }, flightDTO);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var flightDomain = await flightService.GetFlightAsync(id);

            if (flightDomain == null)
                return NotFound();

            return Ok(mapper.Map<GetFlightDTO>(flightDomain));
        }

        [HttpPatch]
        [ValidateUpdateFlight]
        [Authorize(Roles = "Admin, Carrier")]
        public async Task<IActionResult> Update([FromBody] UpdateFlightDTO updateFlight)
        {
            var flightDomain = mapper.Map<Flight>(updateFlight);

            flightDomain = await flightService.UpdateFlightAsync(flightDomain);

            if (flightDomain == null)
                return NotFound();

            return Ok(mapper.Map<GetFlightDTO>(flightDomain));
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Roles = "Admin, Carrier")]
        public async Task<IActionResult> Cancel([FromRoute] Guid id)
        {
            var flightDomain = await flightService.DeleteFlightAsync(id);

            if (flightDomain == null)
                return NotFound();

            if (flightDomain.FlightState != FlightState.SCHEDULED)
                return Conflict();

            return Ok();
        }
    }
}
