using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SpacecraftTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SpacecraftTracker.Persistence
{
    public class SpacecraftTrackerDbContext : IdentityDbContext
    {
        public SpacecraftTrackerDbContext(DbContextOptions<SpacecraftTrackerDbContext> options) : base(options)
        {
            
        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Spacecraft> Spacecrafts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            //Seed

            var adminId = "58506fa8-6559-4f74-925d-456039280a17";
            var carrierId = "f40a4554-5d2a-49fc-b6c8-d01ae53ac296";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = adminId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper(),
                    ConcurrencyStamp = adminId
                },
                new IdentityRole
                {
                    Id = carrierId,
                    Name = "Carrier",
                    NormalizedName = "Carrier".ToUpper(),
                    ConcurrencyStamp = carrierId
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);

            var admin = new IdentityUser
            {
                Id = "a81bd46e-92d7-4e4c-b6aa-91508669bc60",
                UserName = "admin@example.com",
                NormalizedUserName = "admin@example.com".ToUpper(),
                Email = "admin@example.com"
            };
            var hasher = new PasswordHasher<IdentityUser>();
            admin.PasswordHash = hasher.HashPassword(admin, "asdASD123.");
            builder.Entity<IdentityUser>().HasData(admin);

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "58506fa8-6559-4f74-925d-456039280a17",
                    UserId = "a81bd46e-92d7-4e4c-b6aa-91508669bc60"
                }
            );


            var spacecrafts = new List<Spacecraft>
            {
                new Spacecraft
                {
                    Id = Guid.Parse("2551a7b2-bc23-4eb6-a464-a8f1580527de"),
                    Callsign = "Passenger1",
                    Model = "Passenger",
                    Type = SpacecraftType.PASSENGER,
                },
                new Spacecraft
                {
                    Id = Guid.Parse("ffef2a31-b96a-4dfd-a9d0-fe1e4334d7da"),
                    Callsign = "Cargo1",
                    Model = "Cargo",
                    Type = SpacecraftType.CARGO
                }
            };
            builder.Entity<Spacecraft>().HasData(spacecrafts);

            var flights = new List<Flight>
            {
                new Flight
                {
                    Id = Guid.NewGuid(),
                    FlightNumber = "1",
                    OriginName = "Budapest",
                    DepartureDate = DateTime.Parse("2024.10.1"),
                    DestinationName = "Nem Budapest",
                    DestinationDate = DateTime.Parse("2024.10.4"),
                    FlightState = FlightState.SCHEDULED,
                    SpacecraftId = Guid.Parse("ffef2a31-b96a-4dfd-a9d0-fe1e4334d7da")
                },
                new Flight
                {
                    Id = Guid.NewGuid(),
                    FlightNumber = "2",
                    OriginName = "Budapest",
                    DepartureDate = DateTime.Parse("2024.10.2"),
                    DestinationName = "Nem Budapest",
                    DestinationDate = DateTime.Parse("2024.10.5"),
                    FlightState = FlightState.EN_ROUTE,
                    SpacecraftId = Guid.Parse("ffef2a31-b96a-4dfd-a9d0-fe1e4334d7da")
                },
                new Flight
                {
                    Id = Guid.NewGuid(),
                    FlightNumber = "3",
                    OriginName = "Budapest",
                    DepartureDate = DateTime.Parse("2024.10.3"),
                    DestinationName = "Nem Budapest",
                    DestinationDate = DateTime.Parse("2024.10.6"),
                    FlightState = FlightState.ARRIVED,
                    SpacecraftId = Guid.Parse("2551a7b2-bc23-4eb6-a464-a8f1580527de")
                },
            };
            builder.Entity<Flight>().HasData(flights);
        }
    }
}
