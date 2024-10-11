namespace SpacecraftTracker.WebAPI.DTO
{
    public class FlightDTO
    {
        //unique
        public string FlightNumber { get; set; }
        public string OriginName { get; set; }
        public DateTime DepartureDate { get; set; }
        public string DestinationName { get; set; }
        public DateTime DestinationDate { get; set; }
        public Guid SpacecraftId { get; set; }
    }
}
