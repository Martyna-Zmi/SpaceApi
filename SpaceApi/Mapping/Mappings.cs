using SpaceApi.Dtos;
using SpaceApi.Entities;

namespace SpaceApi.Mapping;

public static class Mappings
{
    public static PlanetDto EntityToDto(this Planet planet)
    {
        return new PlanetDto(planet.Id, planet.Name, planet.IsRocky, planet.StarId);
    }
    public static Planet DtoToEntity(this PlanetDto planetDto)
    {
        Planet planet = new()
        {
            Name = planetDto.Name,
            IsRocky = planetDto.IsRocky,
            StarId = planetDto.Star_id
        };
        return planet;
    }
    public static StarDto EntityToDto(this Star star)
    {
        List<PlanetDto> planetDtos = [];
        if (star.Planets != null)
        {
            foreach (var planet in star.Planets)
            {
                planetDtos.Add(planet.EntityToDto());
            }
        }
        return new StarDto(star.Id, star.Name, star.Alias, star.Brightness, star.Radius, planetDtos);
    }
    public static Star SummaryDtoToEntity(this StarSummaryDto starDto)
    {
        var star = new Star
        {
            Name = starDto.Name,
            Alias = starDto.Alias,
            Brightness = starDto.Brightness,
            Radius = starDto.Radius,
        };
        return star;
    }
}
