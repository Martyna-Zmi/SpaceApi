namespace SpaceApi.Entities;

public class Planet
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public bool IsRocky { get; set; }
    public int StarId { get; set; }
    public Star? Star { get; set; }
}
