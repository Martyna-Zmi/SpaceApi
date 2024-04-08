using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public async Task<ActionResult<PlanetDto>> GetAllPlanets(SpaceContext dbContext){
        var found = await dbContext.Planets.Select(planet => planet.EntityToDto()).ToListAsync();
        return Ok(found);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PlanetDto>> GetPlanet(int id, SpaceContext dbContext){
        Planet? planetFound = await dbContext.Planets.FindAsync(id);
        if(planetFound == null) return NotFound("Reason: Couldn't find a planet with such id");
        return Ok(planetFound.EntityToDto());
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PlanetDto>> PostPlanet(PlanetDto planetToCreate, SpaceContext dbContext){
        Planet planet = planetToCreate.DtoToEntity();
        dbContext.Planets.Add(planet);
        var star = await dbContext.Stars.FindAsync(planet.StarId);
        if(star == null) return BadRequest("Reason: Star with that id doesn't exist");
        if(star.Planets == null) star.Planets = [];
        star.Planets.Add(planet);
        dbContext.Stars.Update(star);
        await dbContext.SaveChangesAsync();
        Planet? fromDb = await dbContext.Planets.FirstOrDefaultAsync(planetSearch=> planetSearch.Name==planet.Name);
        if(fromDb == null) return BadRequest();
        return Created(nameof(GetPlanet), new {fromDb.Id});
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PlanetDto>> PutPlanet(int id, PlanetDto putInfo, SpaceContext dbContext){
        Planet? planetFound = await dbContext.Planets.FindAsync(id);
        if(planetFound == null) return BadRequest("Reason: Couldn't modify because such planet doesn't exist");
        planetFound.Name = putInfo.Name;
        planetFound.IsRocky = putInfo.IsRocky;
        planetFound.StarId = putInfo.Star_id;
        var star = await dbContext.Stars.FindAsync(putInfo.Star_id);
        if(star!=null)planetFound.Star = star;
        dbContext.Planets.Update(planetFound);
        await dbContext.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PlanetDto>> DeletePlanet(int id, SpaceContext dbContext){
        Planet? planetFound = await dbContext.Planets.FindAsync(id);
        if(planetFound == null) return NotFound("Reason: Couldn't delete because such planet doesn't exist");
        dbContext.Planets.Remove(planetFound);
        await dbContext.SaveChangesAsync();
        return NoContent();
    }
}
