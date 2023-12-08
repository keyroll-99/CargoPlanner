using CargoApp.Modules.Contracts.Cargoes;
using MediatR;
using Result;
using Result.ApiResult;

namespace CargoApp.Modules.Cargoes.Application.Cargo.FetchById;

public record FetchByIdCommand(Guid Id) : IRequest<ApiResult<CargoDto>>
{
    
}