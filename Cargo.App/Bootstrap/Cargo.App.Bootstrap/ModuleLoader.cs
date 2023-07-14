using System.Reflection;
using Cargo.App.Shared.Abstraction.Module;

namespace Cargo.App.Bootstrap;

internal static class ModuleLoader
{
    public static IList<Assembly> LoadAssemblies(IConfiguration configuration)
    {
        const string modulePart = "Cargo.App.Modules.";

        var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
        var locations = assemblies.Where(x => !x.IsDynamic).Select(x => x.Location).ToArray();
        var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
            .Where(x => !locations.Contains(x, StringComparer.InvariantCultureIgnoreCase))
            .ToList();

        var disabledModules = (from file in files
            where file.Contains(modulePart)
            let moduleName = file.Split(modulePart)[1].Split(".")[0].ToLowerInvariant()
            let enabled = configuration.GetValue<bool>($"{moduleName}:module:enabled")
            where !enabled
            select file).ToList();

        foreach (var disabledModule in disabledModules)
        {
            files.Remove(disabledModule);
        }

        files.ForEach(x => assemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(x))));

        return assemblies;
    }

    public static IList<IModule> LoadModules(IEnumerable<Assembly> assemblies)
        => assemblies
            .SelectMany(x => x.GetTypes())
            .Where(x => typeof(IModule).IsAssignableFrom(x) && !x.IsInterface)
            .OrderBy(x => x.Name)
            .Select(Activator.CreateInstance)
            .Cast<IModule>()
            .ToList();
}
