namespace SpacecraftTracker.WebAPI.DTO
{
    public class GetAllFlightsDTO
    {
        public string FlightNumber { get; set; }
        public string OriginName { get; set; }
        public DateTime DepartureDate { get; set; }
        public string DestinationName { get; set; }
    }
}
