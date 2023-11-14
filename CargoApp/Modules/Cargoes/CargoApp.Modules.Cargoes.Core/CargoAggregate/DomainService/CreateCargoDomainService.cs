using CargoApp.Core.ShareCore.Clock;
using CargoApp.Modules.Cargoes.Core.CompanyAggregate;
using CargoApp.Modules.Cargoes.Core.LocationAggregate;

namespace CargoApp.Modules.Cargoes.Core.CargoAggregate.DomainService;

internal class CreateCargoDomainService : ICreateCargoDomainService
{
    private readonly ILocationRepository _locationRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly ICargoRepository _cargoRepository;
    private readonly IClock _clock;

    public CreateCargoDomainService(
        ILocationRepository locationRepository,
        ICompanyRepository companyRepository,
        ICargoRepository cargoRepository,
        IClock clock)
    {
        _locationRepository = locationRepository;
        _companyRepository = companyRepository;
        _cargoRepository = cargoRepository;
        _clock = clock;
    }

    public async Task<Cargo> CreateCargo(long fromOsmId, long toOsmId, Guid senderId, Guid receiverId,
        DateTime expectedDeliveryTime)
    {
        var from = await _locationRepository.GetByOsmId(fromOsmId);
        var to = await _locationRepository.GetByOsmId(toOsmId);

        var sender = await _companyRepository.GetByCompanyId(senderId);
        var receiver = await _companyRepository.GetByCompanyId(receiverId);

        var cargo = Cargo.Create(from, to, sender, receiver, expectedDeliveryTime, _clock.Now());

        return cargo;
    }
}