using CargoApp.Core.ShareCore.Clock;
using CargoApp.Modules.Cargoes.Core.CargoAggregate;
using CargoApp.Modules.Cargoes.Core.CompanyAggregate;
using CargoApp.Modules.Cargoes.Core.LocationAggregate;
using MediatR;
using Result;

namespace CargoApp.Modules.Cargoes.Application.Cargo.UpdateCargo;

public class UpdateCargoCommandHandler : IRequestHandler<UpdateCargoCommand, Result.Result>
{
    private readonly ICargoRepository _cargoRepository;
    private readonly IClock _clock;
    private readonly ILocationRepository _locationRepository;
    private readonly ICompanyRepository _companyRepository;

    public UpdateCargoCommandHandler(
        ICargoRepository cargoRepository,
        IClock clock,
        ILocationRepository locationRepository,
        ICompanyRepository companyRepository)
    {
        _cargoRepository = cargoRepository;
        _clock = clock;
        _locationRepository = locationRepository;
        _companyRepository = companyRepository;
    }

    public async Task<Result.Result> Handle(UpdateCargoCommand request, CancellationToken cancellationToken)
    {
        Core.LocationAggregate.Location? from = null;
        Core.LocationAggregate.Location? to = null;
        Core.CompanyAggregate.Company? receiver = null;
        
        var cargo = await _cargoRepository.GetByIdAsync(request.Id);
        if (cargo is null)
        {
            return Result.Result.Fail("Cargo not found");
        }

        if (request.FromId is not null)
        {
            from = await _locationRepository.GetByOsmId(request.FromId.Value);
            if (from is null)
            {
                return Result.Result.Fail("Source not found");
            }
        }

        if (request.ToId is not null)
        {
            to = await _locationRepository.GetByOsmId(request.ToId.Value);
            if (to is null)
            {
                return Result.Result.Fail("Destination not found");
            }
        }
        
        if(request.ReceiverId is not null)
        {
            receiver = await _companyRepository.GetByCompanyId(request.ReceiverId.Value);
            if (receiver is null)
            {
                return Result.Result.Fail("Receiver not found");
            }
        }
        
        
        var updateResult = cargo.Update(
            from,
            to,
            receiver,
            request.ExpectedDeliveryTime,
            _clock
        );
        
        await updateResult.OnSuccessAsync(async (_) => { await _cargoRepository.UpdateAsync(cargo); });
        
        return updateResult.IsSuccess
            ? Result.Result.Success()
            : Result.Result.Fail(updateResult.ErrorModel!);
    }
}