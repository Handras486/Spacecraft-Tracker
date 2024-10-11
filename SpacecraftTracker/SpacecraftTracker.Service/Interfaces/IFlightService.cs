using SpacecraftTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpacecraftTracker.Service.Interfaces
{
    public interface IFlightService
    {
        Task<List<Flight>> GetAllFlightsAsync();
        Task<Flight?> GetFlightAsync(Guid id);
        Task<Flight> CreateFlightAsync(Flight flight);
        Task<Flight?> UpdateFlightAsync(Flight flight);
        Task<Flight?> DeleteFlightAsync(Guid id);
    }
}
