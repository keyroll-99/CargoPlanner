using CargoApp.Bootstrap;
using CargoApp.Core.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.LoadModules();

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseInfrastructure();

app.Run();