using CargoApp.Modules.Cargoes.Core.CargoAggregate;
using CargoApp.Modules.Contracts.Cargoes;
using MediatR;
using Result.ApiResult;

namespace CargoApp.Modules.Cargoes.Application.Cargo.FetchCargoesPage;

public class FetchCargoesPageQueryHandler : IRequestHandler<FetchCargoesPageQuery, ApiResult<IList<CargoDto>>>
{
    private readonly ICargoRepository _cargoRepository;

    public FetchCargoesPageQueryHandler(ICargoRepository cargoRepository)
    {
        _cargoRepository = cargoRepository;
    }

    public async Task<ApiResult<IList<CargoDto>>> Handle(FetchCargoesPageQuery request,
        CancellationToken cancellationToken)
    {
        if (request.Page < 0 || request.PageSize < 0)
        {
            return ApiResult<IList<CargoDto>>.Fail("Page and page size must be positive");
        }

        var cargoes = await _cargoRepository.GetPageAsync(request.Page, request.PageSize);

        return cargoes.Select(x => x.CreateDto()).ToList();
    }
}