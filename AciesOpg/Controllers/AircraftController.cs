using AciesOpg.Data;
using Microsoft.AspNetCore.Mvc;
using AciesOpg.Models;

namespace AciesOpg.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AircraftController : ControllerBase
{
    private readonly InMemoryRepository _repository;

    public AircraftController(InMemoryRepository repository)
    {
        _repository = repository;
    }
    
    // GET: api/aircraft
    [HttpGet]
    public ActionResult<IEnumerable<Aircraft>> GetAll()
    {
        return Ok(_repository.GetAllAircraft());
    }
    
    // POST: api/aircraft
    [HttpPost]
    [Route("aircraft")]
    public ActionResult AddAircraft([FromBody] Aircraft aircraft)
    {
        _repository.AddAircraft(aircraft);
        return CreatedAtAction(nameof(GetAll), new { id = aircraft.Name }, aircraft);
    }
}