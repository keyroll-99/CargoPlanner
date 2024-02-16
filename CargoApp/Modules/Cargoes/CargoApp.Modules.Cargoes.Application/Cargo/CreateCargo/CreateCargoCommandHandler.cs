using CargoApp.Core.ShareCore.Clock;
using CargoApp.Modules.Cargoes.Core.CargoAggregate;
using CargoApp.Modules.Cargoes.Core.CompanyAggregate;
using MediatR;
using Result;

namespace CargoApp.Modules.Cargoes.Application.Cargo.CreateCargo;

public class CreateCargoCommandHandler : IRequestHandler<CreateCargoCommand, Result<string>>
{
    private readonly ILocationRepository _locationRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly ICargoRepository _cargoRepository;
    private readonly IClock _clock;

    public CreateCargoCommandHandler(
        ICompanyRepository companyRepository,
        ILocationRepository locationRepository,
        ICargoRepository cargoRepository,
        IClock clock)
    {
        _companyRepository = companyRepository;
        _locationRepository = locationRepository;
        _cargoRepository = cargoRepository;
        _clock = clock;
    }

    public async Task<Result<string>> Handle(CreateCargoCommand request, CancellationToken cancellationToken)
    {
        var from = await _locationRepository.GetByOsmId(request.FromOsmId);
        var to = await _locationRepository.GetByOsmId(request.ToOsmId);

        var sender = await _companyRepository.GetByCompanyId(request.SenderId);
        var receiver = await _companyRepository.GetByCompanyId(request.ReceiverId);


        var createCargoResult = Core.CargoAggregate.Cargo.Create(
            from,
            to,
            sender,
            receiver,
            request.ExpectedDeliveryTime,
            _clock
        );

        await createCargoResult.OnSuccessAsync(async (cargo) => { await _cargoRepository.AddAsync(cargo); });


        return createCargoResult.IsSuccess
            ? Result<string>.Success(createCargoResult.SuccessModel!.Id.ToString())
            : Result<string>.Fail(createCargoResult.ErrorModel!);
    }
}