using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

namespace Cargo.App.Module.User.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        return services;
    }
}