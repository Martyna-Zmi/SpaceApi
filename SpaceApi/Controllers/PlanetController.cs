using Microsoft.AspNetCore.Mvc;
using SpaceApi.Dtos;
using SpaceApi.Entities;

namespace SpaceApi.Controllers;
[ApiController]
[Route("space-api/planets")]
public class PlanetController: ControllerBase
{
    //Temporary List - delete after database creation and implementation
    public List<PlanetDto> TemporaryPlanets = new List<PlanetDto>([new PlanetDto(0, "Earth", true, 0), new PlanetDto(1, "Jupiter", false, 0)]);
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<PlanetDto> GetAllPlanets(){
        return Ok(TemporaryPlanets);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<PlanetDto> GetPlanet(int id){
        return Ok(TemporaryPlanets.Find(planet => planet.Id == id));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public ActionResult<PlanetDto> PostPlanet(PlanetDto planetToCreate){
        return CreatedAtAction(nameof(GetPlanet), new {id = planetToCreate.Id}, planetToCreate);
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<PlanetDto> PutPlanet(int id, PlanetDto putInfo){
        PlanetDto item = TemporaryPlanets.Find(planet => planet.Id == id); //no validation just yet
        TemporaryPlanets.Remove(item);
        TemporaryPlanets.Add(putInfo);
        return Ok(putInfo);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<PlanetDto> DeletePlanet(int id){
        TemporaryPlanets.Remove(TemporaryPlanets.Find(planet=>planet.Id == id)); //no validation just yet
        return Ok();
    }
}
