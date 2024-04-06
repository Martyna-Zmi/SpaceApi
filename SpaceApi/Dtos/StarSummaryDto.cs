using System.ComponentModel.DataAnnotations;
namespace SpaceApi.Dtos;

public record StarSummaryDto( 
[Required] [MaxLength(100)]string Name, 
[Required][MaxLength(100)] string Alias, 
[Required] float Brightness, //in apparent magnitude
[Required] [Range(0, int.MaxValue)] double Radius); //in sun radius 
