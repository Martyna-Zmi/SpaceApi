using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace SpaceApi.Entities;

public class Planet
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsRocky { get; set; }
    public int StarId { get; set; }
    public Star Star { get; set; } = null!;

    public Planet(){

    }
    public Planet(int id, string name, bool isRocky, int starId, Star star){
        Id = id;
        Name = name;
        IsRocky = isRocky;
        StarId = starId;
        Star = star;
    }
}
