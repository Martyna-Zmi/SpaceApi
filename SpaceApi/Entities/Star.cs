namespace SpaceApi.Entities;

public class Star
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Alias { get; set; }
    public float Brightness { get; set; }
    public double Radius { get; set; }
    public List<Planet>? Planets { get; set;}
}
