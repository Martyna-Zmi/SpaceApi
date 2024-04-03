using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SpaceApi.Dtos;

public record StarDto(int Id, 
[Required] [MaxLength(100)]string Name, 
[MaxLength(100)] string? Alias, 
[Required] float Brightness, //in apparent magnitude
[Required] [Range(0, int.MaxValue)] double Radius, //in sun radius
IList<PlanetDto>? Planets); 
