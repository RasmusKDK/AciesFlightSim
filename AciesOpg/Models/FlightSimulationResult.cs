namespace AciesOpg.Models;

public class FlightSimulationResult
{
    public double DistanceKm { get; set; }
    public double EstimatedTimeHours { get; set; }
    public double FuelUsedLiters { get; set; }
    public double FuelUsedWithReserveLiters { get; set; }
    public string StatusMessage { get; set; }
}