using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SpaceApi.Dtos;

public record PlanetDto(
    int Id,
    [Required][MaxLength(100)] string Name,
    [Required][property: JsonPropertyName("is_rocky")] bool IsRocky,
    [Required][property: JsonPropertyName("star_id")] int Star_id);
