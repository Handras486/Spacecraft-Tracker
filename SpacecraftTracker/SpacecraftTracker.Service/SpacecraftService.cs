using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
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
    public class SpacecraftService : ISpacecraftService
    {
        private readonly SpacecraftTrackerDbContext dbContext;
        private readonly IMapper mapper;

        public SpacecraftService(SpacecraftTrackerDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<Spacecraft> CreateSpacecraftAsync(Spacecraft spacecraft)
        {
            await dbContext.Spacecrafts.AddAsync(spacecraft);
            await dbContext.SaveChangesAsync();
            return spacecraft;
        }

        public async Task<Spacecraft?> DeleteSpacecraftAsync(Guid id)
        {
            var existingSpaceCraft = await dbContext.Spacecrafts.FirstOrDefaultAsync(x => x.Id == id);

            if (existingSpaceCraft == null)
                return null;

            dbContext.Spacecrafts.Remove(existingSpaceCraft);
            await dbContext.SaveChangesAsync();

            return existingSpaceCraft;
        }

        public async Task<List<Spacecraft>> GetAllSpacecraftsAsync()
        {
            var spacecrafts = dbContext.Spacecrafts.AsQueryable();

            return await spacecrafts.OrderBy(x => x.Callsign).ToListAsync();
        }

        public async Task<Spacecraft?> GetSpacecraftAsync(Guid id)
        {
            return await dbContext.Spacecrafts.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Spacecraft?> UpdateSpacecraftAsync(Spacecraft spacecraft)
        {
            var existingSpacecraft = await dbContext.Spacecrafts.FirstOrDefaultAsync(x => x.Id == spacecraft.Id);

            if (existingSpacecraft == null)
                return null;

            mapper.Map(spacecraft, existingSpacecraft);

            await dbContext.SaveChangesAsync();
            return existingSpacecraft;
        }
    }
}
