namespace SpaceApi.Entities;

public class Star
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Alias { get; set; }
    public required float Brightness { get; set; }
    public required double Radius { get; set; }
    public IList<Planet>? Planets { get; set;}
}
