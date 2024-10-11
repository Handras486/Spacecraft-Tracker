using SpacecraftTracker.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace SpacecraftTracker.WebAPI.DTO
{
    public class CreateSpacecraftDTO
    {
        [Range(0, 1)]
        public SpacecraftType Type { get; set; }
        //unique
        public string Callsign { get; set; }
        public string Model { get; set; }
    }
}
