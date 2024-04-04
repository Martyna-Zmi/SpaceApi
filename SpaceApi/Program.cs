using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpaceApi.Data;
[assembly: ApiController]

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("SpaceApi");
builder.Services.AddSqlite<SpaceContext>(connString);

builder.Services.AddControllers();

var app = builder.Build();

app.MigrateDbOnStartup();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();