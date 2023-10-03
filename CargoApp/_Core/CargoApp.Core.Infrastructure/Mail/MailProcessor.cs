using System.Reflection;
using RazorLight;

namespace CargoApp.Core.Infrastructure.Mail;

public class MailProcessor : IMailProcessor
{
    private readonly RazorLightEngineBuilder _builder = new RazorLightEngineBuilder();

    public async Task<string> Process<T>(T model)
    {
        var modelType = typeof(T);
        
        var engine = _builder
            .UseEmbeddedResourcesProject(modelType)
            .SetOperatingAssembly(modelType.Assembly)
            .Build();

        var result = await engine.CompileRenderAsync(modelType.Name, model);
        return result;
    }
}