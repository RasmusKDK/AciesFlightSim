using AciesOpg.Data;
using Microsoft.AspNetCore.Mvc;
using AciesOpg.Models;

namespace AciesOpg.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AirportController : ControllerBase
{
    private readonly InMemoryRepository _repository;

    public AirportController(InMemoryRepository repository)
    {
        _repository = repository;
    }
    
    // GET: api/airport
    [HttpGet]
    public ActionResult<IEnumerable<Airport>> GetAll()
    {
        return Ok(_repository.GetAllAirports());
    }
    
    // POST: api/aircraft
    [HttpPost]
    [Route("airport")]
    public ActionResult AddAirport([FromBody] Airport airport)
    {
        _repository.AddAirport(airport);
        return CreatedAtAction(nameof(GetAll), new { id = airport.Name }, airport);
    }
}