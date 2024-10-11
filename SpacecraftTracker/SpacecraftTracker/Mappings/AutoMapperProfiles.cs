using AutoMapper;
using SpacecraftTracker.Domain.Entities;
using SpacecraftTracker.WebAPI.DTO;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace SpacecraftTracker.WebAPI.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CreateSpacecraftDTO, Spacecraft>();
            CreateMap<Spacecraft, GetAllSpacecraftsDTO>();
            CreateMap<Spacecraft, SpacecraftDTO>().ReverseMap();
            CreateMap<Spacecraft, Spacecraft>();

            CreateMap<UpdateFlightDTO, Flight>();
            CreateMap<FlightDTO, Flight>().ReverseMap();
            CreateMap<Flight, GetFlightDTO>();
            CreateMap<Flight, GetAllFlightsDTO>();
            CreateMap<Flight, Flight>();
        }

    }
}
