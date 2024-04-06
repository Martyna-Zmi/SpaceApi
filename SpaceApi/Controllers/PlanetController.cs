using Microsoft.AspNetCore.Mvc;
using SpaceApi.Data;
using SpaceApi.Dtos;
using SpaceApi.Entities;
using SpaceApi.Mapping;

namespace SpaceApi.Controllers;
[ApiController]
[Route("space-api/planets")]
public class PlanetController: ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<PlanetDto> GetAllPlanets(SpaceContext dbContext){
        return Ok(dbContext.Planets.Select(planet => planet.EntityToDto()).ToList());
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<PlanetDto> GetPlanet(int id, SpaceContext dbContext){
        Planet? planetFound = dbContext.Planets.Find(id);
        if(planetFound == null) return NotFound("Reason: Couldn't find a planet with such id");
        return Ok(planetFound.EntityToDto());
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<PlanetDto> PostPlanet(PlanetDto planetToCreate, SpaceContext dbContext){
        Planet planet = planetToCreate.DtoToEntity();
        dbContext.Planets.Add(planet);
        var star = dbContext.Stars.Find(planet.StarId);
        if(star == null) return BadRequest("Reason: Star with that id doesn't exist");
        if(star.Planets == null) star.Planets = [];
        star.Planets.Add(planet);
        dbContext.Stars.Update(star);
        dbContext.SaveChanges();
        return CreatedAtAction(nameof(GetPlanet), new {id = planetToCreate.Id}, planetToCreate);
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<PlanetDto> PutPlanet(int id, PlanetDto putInfo, SpaceContext dbContext){
        Planet? planetFound = dbContext.Planets.Find(id);
        if(planetFound == null) return BadRequest("Reason: Couldn't modify because such planet doesn't exist");
        planetFound.Name = putInfo.Name;
        planetFound.IsRocky = putInfo.IsRocky;
        planetFound.StarId = putInfo.Star_id;
        planetFound.Star = dbContext.Stars.Find(putInfo.Star_id);
        dbContext.Planets.Update(planetFound);
        dbContext.SaveChanges();
        return Ok();
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<PlanetDto> DeletePlanet(int id, SpaceContext dbContext){
        Planet? planetFound = dbContext.Planets.Find(id);
        if(planetFound == null) return NotFound("Reason: Couldn't delete because such planet doesn't exist");
        dbContext.Planets.Remove(planetFound);
        dbContext.SaveChanges();
        return Ok();
    }
}
