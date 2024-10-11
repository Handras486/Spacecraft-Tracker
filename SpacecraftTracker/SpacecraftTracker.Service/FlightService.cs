using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SpacecraftTracker.Domain.Entities;
using SpacecraftTracker.Persistence;
using SpacecraftTracker.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpacecraftTracker.Service
{
    public class FlightService : IFlightService
    {
        private readonly SpacecraftTrackerDbContext dbContext;
        private readonly IMapper mapper;

        public FlightService(SpacecraftTrackerDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<Flight> CreateFlightAsync(Flight flight)
        {
            //check at validation
            flight.Spacecraft = await dbContext.Spacecrafts.FirstOrDefaultAsync(x => x.Id == flight.SpacecraftId);

            await dbContext.Flights.AddAsync(flight);
            await dbContext.SaveChangesAsync();
            return flight;
        }

        public async Task<Flight?> DeleteFlightAsync(Guid id)
        {
            var existingFlight = await dbContext.Flights.FirstOrDefaultAsync(x => x.Id == id);

            if (existingFlight == null)
                return null;

            dbContext.Flights.Remove(existingFlight);
            await dbContext.SaveChangesAsync();

            return existingFlight;

        }

        public async Task<List<Flight>> GetAllFlightsAsync()
        {
            var flights = dbContext.Flights.AsQueryable();

            return await flights.OrderByDescending(x => x.DepartureDate).ToListAsync();
        }

        public async Task<Flight?> GetFlightAsync(Guid id)
        {
            var flight = await dbContext.Flights.FirstOrDefaultAsync(x => x.Id == id);
            flight.Spacecraft = await dbContext.Spacecrafts.FirstOrDefaultAsync(x => x.Id == flight.SpacecraftId);
            return flight;
        }

        public async Task<Flight?> UpdateFlightAsync(Flight flight)
        {
            var existingFlight = await dbContext.Flights.FirstOrDefaultAsync(x => x.Id == flight.Id);

            if (existingFlight == null)
                return null;


            flight.Spacecraft = await dbContext.Spacecrafts.FirstOrDefaultAsync(x => x.Id == flight.SpacecraftId);

            //mapper.Map(flight, existingFlight);

            existingFlight.FlightNumber = flight.FlightNumber;
            existingFlight.OriginName = flight.OriginName;
            existingFlight.DepartureDate = flight.DepartureDate;
            existingFlight.DestinationDate = flight.DestinationDate;
            existingFlight.DestinationName = flight.DestinationName;
            existingFlight.FlightState = flight.FlightState;
            existingFlight.SpacecraftId = flight.SpacecraftId;
            existingFlight.Spacecraft = flight.Spacecraft;

            await dbContext.SaveChangesAsync();
            return existingFlight;
        }
    }
}
