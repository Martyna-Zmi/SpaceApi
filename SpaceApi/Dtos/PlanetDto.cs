using System.ComponentModel.DataAnnotations;

namespace SpaceApi.Dtos;

public record PlanetDto(
    int Id, 
    [Required] [MaxLength(100)] string Name, 
    [Required] bool IsRocky);
