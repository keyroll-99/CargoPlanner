using CargoApp.Core.Abstraction.Context;
using Microsoft.AspNetCore.Http;

namespace CargoApp.Core.Infrastructure.Context;

public class Context : IContext
{
    public string RequestId { get; } = $"{Guid.NewGuid():N}";
    public string TraceId { get; }
    public IIdentityContext IdentityContext { get; }

    public Context(HttpContext context)
    {
        TraceId = context.TraceIdentifier;
        IdentityContext = new IdentityContext(context.User);
    }

    private Context()
    {
        
    }

    public static IContext Empty() => new Context();
}