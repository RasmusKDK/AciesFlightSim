namespace AciesOpg.Models;

public class Aircraft
{
    public string Id { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }
    public double EmptyWeightKg { get; set; }
    public double MaxTakeoffWeightKg { get; set; }
    public double FuelEfficiency { get; set; } //Kilometer pr liter
    public double MaxSpeedKmh { get; set; }
    public int RequiredCrew { get; set; }
    public double FuelCapacityLiters { get; set; }
}