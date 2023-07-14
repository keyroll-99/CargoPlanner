using Cargo.App.Bootstrap;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

var assemblies = ModuleLoader.LoadAssemblies(configuration);
var modules = ModuleLoader.LoadModules(assemblies);


var app = builder.Build();

app.Run();