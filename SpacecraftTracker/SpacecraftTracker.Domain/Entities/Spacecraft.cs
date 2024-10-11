using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpacecraftTracker.Domain.Entities
{
    public enum SpacecraftType
    {
        PASSENGER,
        CARGO
    }

    public class Spacecraft
    {
        public Guid Id { get; set; }
        public SpacecraftType Type { get; set; }
        //unique
        public string Callsign { get; set; }
        public string Model { get; set; }
        public ICollection<Flight> Flights { get; } = new List<Flight>();
    }
}
