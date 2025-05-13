namespace AciesOpg.Models;


public class FlightSimulationRequest
{
    public string AircraftId { get; set; }
    public string FromAirportId { get; set; }
    public string ToAirportId { get; set; }
}