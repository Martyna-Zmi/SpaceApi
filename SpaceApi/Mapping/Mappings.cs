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
        List<PlanetDto>? planets = (star.Planets==null)? null: star.Planets.ConvertAll<PlanetDto>(planet => planet.EntityToDto());
        return new StarDto(star.Id, star.Name, star.Alias, star.Brightness, star.Radius, planets);
    }
    public static Star DtoToEntity(this StarDto starDto){
        List<Planet>? planets = (starDto.Planets==null)? null: starDto.Planets.ConvertAll<Planet>(planet=>planet.DtoToEntity());
        var star = new Star
        {
            Name = starDto.Name,
            Alias = starDto.Alias,
            Brightness = starDto.Brightness,
            Radius = starDto.Radius,
            Planets = planets
        };
        return star;
    }
}
