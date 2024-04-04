using Microsoft.AspNetCore.Mvc;
using SpaceApi.Dtos;
using SpaceApi.Entities;

namespace SpaceApi.Controllers;
[ApiController]
[Route("space-api/planets")]
public class PlanetController: ControllerBase
{
    //Temporary List - delete after database creation and implementation
    public static List<PlanetDto> TemporaryPlanets = new List<PlanetDto>([new PlanetDto(0, "Earth", true, 0), new PlanetDto(1, "Jupiter", false, 0)]);
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<PlanetDto> GetAllPlanets(){
        return Ok(TemporaryPlanets);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<PlanetDto> GetPlanet(int id){
        PlanetDto? planet = TemporaryPlanets.Find(planet => planet.Id == id);
        if(planet == null) return NotFound("Reason: Couldn't find a planet with such id");
        return Ok(planet);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public ActionResult<PlanetDto> PostPlanet(PlanetDto planetToCreate){
        return CreatedAtAction(nameof(GetPlanet), new {id = planetToCreate.Id}, planetToCreate);
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<PlanetDto> PutPlanet(int id, PlanetDto putInfo){
        PlanetDto? planet = TemporaryPlanets.Find(planet => planet.Id == id);
        if(TemporaryPlanets.Find(planet=>planet.Id == id)==null) return BadRequest("Reason: Couldn't modify because such planet doesn't exist");
        TemporaryPlanets.Remove(planet);
        TemporaryPlanets.Add(putInfo);
        return Ok(putInfo);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<PlanetDto> DeletePlanet(int id){
        PlanetDto? planet = TemporaryPlanets.Find(planet=>planet.Id == id);
        if(planet == null) return NotFound("Reason: Couldn't delete because such planet doesn't exist");
        TemporaryPlanets.Remove(planet);
        return Ok();
    }
}
