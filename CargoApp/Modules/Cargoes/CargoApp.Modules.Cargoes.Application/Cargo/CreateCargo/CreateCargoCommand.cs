using MediatR;
using Result;

namespace CargoApp.Modules.Cargoes.Application.Cargo.CreateCargo;

public record CreateCargoCommand(
    long FromOsmId,
    long ToOsmId,
    Guid SenderId,
    Guid ReceiverId,
    DateTime ExpectedDeliveryTime
    ) : IRequest<Result<string>>
{
    
}