using AciesOpg.Data;
using AciesOpg.Models;
using AciesOpg.Utils;
using Microsoft.AspNetCore.Mvc;



namespace AciesOpg.Controllers;


[ApiController]
[Route("[controller]")]
public class SimulationController : ControllerBase
{
    private readonly InMemoryRepository _repo;

    public SimulationController(InMemoryRepository repo)
    {
        _repo = repo;
    }

    [HttpPost("simulate")]
    public IActionResult SimulateFlight([FromBody] FlightSimulationRequest request)
    {
        var aircraft = _repo.GetAllAircraft().FirstOrDefault(a => a.Id == request.AircraftId);
        var airports = _repo.GetAllAirports();
        var from = airports.FirstOrDefault(a => a.Id == request.FromAirportId);
        var to = airports.FirstOrDefault(a => a.Id == request.ToAirportId);

        if (aircraft == null || from == null || to == null)
            return NotFound("ugyldige ID'er for fly eller lufthavne");

        double distanceKm = GeoUtils.CalculateDistanceKm(from.Latitude, from.Longitude, to.Latitude, to.Longitude);
        double flightTimeHours = distanceKm / aircraft.MaxSpeedKmh;
        double fuelUsed = distanceKm * aircraft.FuelEfficiency;

        //10% reserve fuel - ICAO standarter
        const double fuelReserveFactor = 1.10;
        double fuelUsedWithReserve = fuelUsed * fuelReserveFactor;
        
        //Tjek om flyet kan klare distancen inkl.reserve
        string statusMessage;
        if (fuelUsedWithReserve > aircraft.FuelCapacityLiters)
        {
                statusMessage = $"Flyet kan ikke nå destinationen. Det kræver {fuelUsed:F2} liter, men kan kun bære {aircraft.FuelCapacityLiters} liter brændstof";
        } else
        {
            statusMessage = "Flyet kan godt nå destinationen med den nødvendige mængde brændstof";
        }

        var result = new FlightSimulationResult
        {
            DistanceKm = distanceKm,
            EstimatedTimeHours = flightTimeHours,
            FuelUsedLiters = fuelUsed,
            FuelUsedWithReserveLiters = fuelUsedWithReserve,
            StatusMessage = statusMessage
        };

        return Ok(result);
    }
}