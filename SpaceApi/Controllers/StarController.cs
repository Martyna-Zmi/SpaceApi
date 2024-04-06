using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpaceApi.Data;
using SpaceApi.Dtos;
using SpaceApi.Entities;
using SpaceApi.Mapping;

namespace SpaceApi.Controllers;

[ApiController]
[Route("space-api/stars")]
public class StarController : ControllerBase
{
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<StarDto> GetStar(int id, SpaceContext dbContext)
    {
        var starFound = dbContext.Stars.Find(id);
        if(starFound == null) return NotFound("Reason: Couldn't find a star with such id");
        var planets = from p in dbContext.Planets
                   where p.StarId.Equals(id)
                   select p;
        starFound.Planets = planets.Select(p => new Planet(p.Id, p.Name, p.IsRocky, p.StarId, p.Star)).ToList();
        StarDto dto = starFound.EntityToDto();
        return Ok(dto);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<List<StarDto>> GetAllStars(SpaceContext dbContext)
    {
        return Ok(dbContext.Stars.Include(star=>star.Planets).Select(star => star.EntityToDto()).ToList());
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<StarDto> PostStar(StarSummaryDto starToCreate, SpaceContext dbContext)
    {
        Star star = starToCreate.SummaryDtoToEntity();
        dbContext.Stars.Add(star);
        if(star.Planets != null) dbContext.Planets.AddRange(star.Planets);
        dbContext.SaveChanges();
        Star? fromDb = dbContext.Stars.FirstOrDefault(starSearch=> starSearch.Name==star.Name);
        if(fromDb == null) return BadRequest();
        return Created(nameof(GetStar), new {fromDb.Id});
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<StarDto> PutStar(int id, StarSummaryDto putInfo, SpaceContext dbContext)
    {
        var starFound = dbContext.Stars.Find(id);
        if(starFound == null) return BadRequest("Reason: Couldn't modify because this star doesn't exist");
        starFound.Name = putInfo.Name;
        starFound.Alias = putInfo.Alias;
        starFound.Brightness = putInfo.Brightness;
        starFound.Radius = putInfo.Radius;
        dbContext.Stars.Update(starFound);
        dbContext.SaveChanges();
        return NoContent();
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<StarDto> DeleteStar(int id, SpaceContext dbContext)
    {
        var star = dbContext.Stars.Find(id);
        if(star == null) return NotFound("Reason: Couln't delete because such star doesn't exist");
        dbContext.Stars.Remove(star);
        dbContext.SaveChanges();
        return NoContent();
    }
}
