using System.Runtime.CompilerServices;
using System.Text.Json;
using CargoApp.Core.Infrastructure.Auth;
using CargoApp.Core.Infrastructure.Context;
using CargoApp.Core.Infrastructure.Mail;
using CargoApp.Core.Infrastructure.Metadata;
using CargoApp.Core.Infrastructure.Postgres;
using CargoApp.Core.Infrastructure.Rabbit;
using CargoApp.Core.Infrastructure.Repositories;
using CargoApp.Core.ShareCore.Clock;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

[assembly: InternalsVisibleTo("CargoApp.Bootstrap")]

namespace CargoApp.Core.Infrastructure;

public static class Extensions
{
    internal static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApiDevoTo", Version = "v1" });
        });
        services.AddSingleton<IClock, Clock.Clock>();
        services.AddControllers()
            .AddJsonOptions(o => o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);
        services.AddEndpointsApiExplorer();
        services.AddPostgres();
        services.AddAuth();
        services.AddContext();
        services.AddRabbit();
        services.AddMail();
        services.LoadMetadata();

        services.AddCors((setupAction) =>
        {
            setupAction.AddPolicy("local",
                builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .WithOrigins("http://localhost:4200");
                });
        });
        
        return services;
    }


    internal static WebApplication UseInfrastructure(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseReDoc(c =>
            {
                c.DocumentTitle = "REDOC API Documentation";
                c.SpecUrl = "/swagger/v1/swagger.json";
            });

            app.UseCors("local");
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }

    public static T GetOptions<T>(this IServiceCollection services, string sectionName) where T : new()
    {
        using var serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        return configuration.GetOptions<T>(sectionName);
    }

    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : new()
    {
        var option = new T();
        configuration.GetSection(sectionName).Bind(option);
        return option;
    }
}