using SpacecraftTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SpacecraftTracker.Service.Interfaces
{
    public interface ISpacecraftService
    {
        Task<List<Spacecraft>> GetAllSpacecraftsAsync();
        Task<Spacecraft?> GetSpacecraftAsync(Guid id);
        Task<Spacecraft> CreateSpacecraftAsync(Spacecraft spacecraft);
        Task<Spacecraft?> UpdateSpacecraftAsync(Spacecraft spacecraft);
        Task<Spacecraft?> DeleteSpacecraftAsync(Guid id);
 
    }
}
