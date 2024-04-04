using Microsoft.EntityFrameworkCore;
using SpaceApi.Entities;

namespace SpaceApi.Data;

public class SpaceContext(DbContextOptions<SpaceContext> Options)
: DbContext(Options)
{
    public DbSet<Star> Stars => Set<Star>();
    public DbSet<Planet> Planets => Set<Planet>();
}
