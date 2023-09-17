using CargoApp.Bootstrap;
using CargoApp.Core.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration);
});


builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.LoadModules();

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSerilogRequestLogging();
app.UseInfrastructure();

app.Run();