using MediatR;

namespace CargoApp.Modules.Cargoes.Application.Cargo.UpdateCargo;

public record UpdateCargoCommand(
    Guid Id,
    long? FromId,
    long? ToId,
    Guid? ReceiverId,
    DateTime? ExpectedDeliveryTime): IRequest<Result.Result>
{
    
}