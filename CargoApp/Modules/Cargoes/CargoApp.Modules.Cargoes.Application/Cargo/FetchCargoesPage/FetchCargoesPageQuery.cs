using CargoApp.Modules.Contracts.Cargoes;
using MediatR;
using Result.ApiResult;

namespace CargoApp.Modules.Cargoes.Application.Cargo.FetchCargoesPage;

public record FetchCargoesPageQuery(int Page, int PageSize) : IRequest<ApiResult<IList<CargoDto>>>;