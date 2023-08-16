using CargoApp.Core.Infrastructure.CQRS.Request;

namespace CargoApp.Modules.Locations.Application.Commands.AddLocationCommand;

public interface IAddLocationCommandHandler : IRequestHandler<AddLocationCommand, string, string>
{
    
}