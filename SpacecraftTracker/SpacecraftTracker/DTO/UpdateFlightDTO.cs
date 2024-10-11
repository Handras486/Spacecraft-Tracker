using SpacecraftTracker.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace SpacecraftTracker.WebAPI.DTO
{
    public class UpdateFlightDTO
    {
        public Guid Id { get; set; }
        //unique
        public string FlightNumber { get; set; }
        public string OriginName { get; set; }
        public DateTime DepartureDate { get; set; }
        public string DestinationName { get; set; }
        public DateTime DestinationDate { get; set; }
        [Range(0, 2)]
        public FlightState FlightState { get; set; } 
        public Guid SpacecraftId { get; set; }
    }
}
