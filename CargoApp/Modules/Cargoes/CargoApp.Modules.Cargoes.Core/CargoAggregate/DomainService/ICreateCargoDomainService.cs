namespace CargoApp.Modules.Cargoes.Core.CargoAggregate.DomainService;

public interface ICreateCargoDomainService
{
    public Task<Cargo> CreateCargo(long fromOsmId, long toOsmId, Guid senderId, Guid receiverId,
        DateTime expectedDeliveryTime);
}