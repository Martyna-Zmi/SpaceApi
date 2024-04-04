using Microsoft.EntityFrameworkCore;

namespace SpaceApi.Data;

public static class DataExtensions
{
 public static void MigrateDbOnStartup(this WebApplication app)
 {
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<SpaceContext>();
    dbContext.Database.Migrate();
 }
}
