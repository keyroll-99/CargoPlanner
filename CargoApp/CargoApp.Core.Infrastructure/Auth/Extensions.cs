using System.Text;
using CargoApp.Core.Abstraction.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CargoApp.Core.Infrastructure.Auth;

public static class Extensions
{
    public static IServiceCollection AddAuth(this IServiceCollection services)
    {
        var options = services.GetOptions<AuthOptions>("Jwt");
        services.AddSingleton<IAuthManager, AuthManager>();

        var tokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.IssuerSigningKey)),
            ValidateIssuer = options.ValidateIssuer,
            ValidIssuer = options.ValidIssuer,
            ValidateAudience = options.ValidateAudience,
            ClockSkew = TimeSpan.Zero,
            ValidateLifetime = options.ValidateLifetime,
        };

        services.AddAuthentication(o =>
        {
            o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.TokenValidationParameters = tokenValidationParameters;
        });

        services.AddSingleton(options);
        
        return services;
    }
}