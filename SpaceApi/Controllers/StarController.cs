using Microsoft.AspNetCore.Mvc;
using SpaceApi.Dtos;
using SpaceApi.Entities;

namespace SpaceApi.Controllers;

[ApiController]
[Route("space-api/stars")]
public class StarController : ControllerBase
{
    //Temporary List - delete after database creation and implementation
    public List<StarDto> TemporatyStars = new List<StarDto>([
        new StarDto(0, "Sun", "Sun", -26.7F, 1, new List<PlanetDto>([new PlanetDto(0, "Earth", true, 0)])),
        new StarDto(1, "Sirius A", "Alpha Canis Majoris [A]", -1.46F, 1.711, new List<PlanetDto>()),
        new StarDto(2, "Alpha Centauri A", "Alpha Centauri [A]", 1.33F, 1.1, new List<PlanetDto>())]);

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<StarDto> GetStar(int id)
    {
        return Ok(TemporatyStars.Find(star => star.Id == id));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<List<StarDto>> GetAllStars()
    {
        return Ok(TemporatyStars);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public ActionResult<StarDto> PostStar(StarDto starToCreate)
    {
        TemporatyStars.Add(starToCreate);
        return CreatedAtAction(nameof(GetStar), new { id = starToCreate.Id }, starToCreate);
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<StarDto> PutStar(int id, StarDto putInfo)
    {
        StarDto item = TemporatyStars.Find(star => star.Id == id); //no validation just yet
        TemporatyStars.Remove(item);
        TemporatyStars.Add(putInfo);
        return Ok(putInfo);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<StarDto> DeleteStar(int id)
    {
        TemporatyStars.Remove(TemporatyStars.Find(star => star.Id == id)); //no validation just yet
        return Ok();
    }
}
