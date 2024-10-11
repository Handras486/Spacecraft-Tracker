using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpacecraftTracker.Domain.Entities
{
    public enum FlightState
    {
        SCHEDULED,
        EN_ROUTE,
        ARRIVED
    }

    public class Flight
    {
        public Guid Id { get; set; }
        //unique
        public string FlightNumber { get; set; }
        public string OriginName { get; set; }
        public DateTime DepartureDate { get; set; }
        public string DestinationName { get; set; }
        public DateTime DestinationDate { get; set; }
        public FlightState FlightState { get; set; } = FlightState.SCHEDULED;
        public Guid SpacecraftId { get; set; }
        public Spacecraft Spacecraft { get; set; } = null!;
    }
}
