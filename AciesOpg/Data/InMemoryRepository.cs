using System.Text.Json;
using AciesOpg.Models;



namespace AciesOpg.Data;

public class InMemoryRepository
{
    private readonly string _aircraftFile = "Data/aircraft.json";
    private List<Aircraft> _aircrafts;

    private readonly string _airportFile = "Data/airport.json";
    private List<Airport> _airports;


    public InMemoryRepository()
    {
        if (File.Exists(_aircraftFile))
        {
            string json = File.ReadAllText(_aircraftFile);
            _aircrafts = JsonSerializer.Deserialize<List<Aircraft>>(json) ?? new List<Aircraft>();
        }
        else
        {
            _aircrafts = new List<Aircraft>();
        }

        if (File.Exists(_airportFile))
        {
            string json = File.ReadAllText(_airportFile);
            _airports = JsonSerializer.Deserialize<List<Airport>>(json) ?? new List<Airport>();
        }
        else
        {
            _airports = new List<Airport>();
        }
    }

    public List<Aircraft> GetAllAircraft() => _aircrafts;

    public void AddAircraft(Aircraft aircraft)
    {
        if (string.IsNullOrWhiteSpace(aircraft.Id))
        {
            aircraft.Id = Guid.NewGuid().ToString();
        }
        
        _aircrafts.Add(aircraft);
        SaveAircrafts();
    }

    private void SaveAircrafts()
    {
        string json = JsonSerializer.Serialize(_aircrafts, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_aircraftFile, json);
    }

    public List<Airport> GetAllAirports() => _airports;

    public void AddAirport(Airport airport)
    {
        if (string.IsNullOrWhiteSpace(airport.Id))
        {
            airport.Id = Guid.NewGuid().ToString();
        }
        
        _airports.Add(airport);
        SaveAirports();
    }

    private void SaveAirports()
    {
        string json = JsonSerializer.Serialize(_airports, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_airportFile, json);
    }
    
    
}