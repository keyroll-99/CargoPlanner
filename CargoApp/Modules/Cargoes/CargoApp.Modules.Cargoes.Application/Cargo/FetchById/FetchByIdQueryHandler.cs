using CargoApp.Modules.Cargoes.Core.CargoAggregate;
using CargoApp.Modules.Contracts.Cargoes;
using MediatR;
using Microsoft.AspNetCore.Http;
using Result;
using Result.ApiResult;

namespace CargoApp.Modules.Cargoes.Application.Cargo.FetchById;

public class FetchByIdQueryHandler : IRequestHandler<FetchByIdQuery, ApiResult<CargoDto>>
{
    private readonly ICargoRepository _cargoRepository;

    public FetchByIdQueryHandler(ICargoRepository cargoRepository)
    {
        _cargoRepository = cargoRepository;
    }

    public async Task<ApiResult<CargoDto>> Handle(FetchByIdQuery request,
        CancellationToken cancellationToken)
    {
        var cargo = await _cargoRepository.GetByIdAsync(request.Id);
        if (cargo is null)
        {
            return ApiResult<CargoDto>.Fail("Cargo not found", StatusCodes.Status404NotFound);
        }

        return cargo.CreateDto();
    }
}