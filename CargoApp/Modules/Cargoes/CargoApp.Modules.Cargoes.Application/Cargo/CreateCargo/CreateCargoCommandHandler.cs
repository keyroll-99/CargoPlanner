using CargoApp.Modules.Cargoes.Core.CargoAggregate;
using CargoApp.Modules.Cargoes.Core.CargoAggregate.DomainService;
using CargoApp.Modules.Cargoes.Core.CompanyAggregate;
using CargoApp.Modules.Cargoes.Core.LocationAggregate;
using MediatR;
using Result;

namespace CargoApp.Modules.Cargoes.Application.Cargo.CreateCargo;

public class CreateCargoCommandHandler : IRequestHandler<CreateCargoCommand, Result<string>>
{
    private readonly ICreateCargoDomainService _createCargoDomainService;
    private readonly ILocationRepository _locationRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly ICargoRepository _cargoRepository;

    public CreateCargoCommandHandler(
        ICreateCargoDomainService createCargoDomainService,
        ICompanyRepository companyRepository,
        ILocationRepository locationRepository,
        ICargoRepository cargoRepository)
    {
        _createCargoDomainService = createCargoDomainService;
        _companyRepository = companyRepository;
        _locationRepository = locationRepository;
        _cargoRepository = cargoRepository;
    }

    public async Task<Result<string>> Handle(CreateCargoCommand request, CancellationToken cancellationToken)
    {
        var from = await _locationRepository.GetByOsmId(request.FromOsmId);
        var to = await _locationRepository.GetByOsmId(request.ToOsmId);

        var sender = await _companyRepository.GetByCompanyId(request.SenderId);
        var receiver = await _companyRepository.GetByCompanyId(request.ReceiverId);


        var createCargoResult = _createCargoDomainService.CreateCargo(
            from,
            to,
            sender,
            receiver,
            request.ExpectedDeliveryTime
        );

        await createCargoResult.OnSuccessAsync(async (cargo) => { await _cargoRepository.AddAsync(cargo); });


        return createCargoResult.IsSuccess
            ? Result<string>.Success(createCargoResult.SuccessModel!.Id.ToString())
            : Result<string>.Fail(createCargoResult.ErrorModel!);
    }
}