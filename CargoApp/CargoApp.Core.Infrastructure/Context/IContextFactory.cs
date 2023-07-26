using CargoApp.Core.Abstraction.Context;

namespace CargoApp.Core.Infrastructure.Context;

internal interface IContextFactory
{
    IContext Create();
}