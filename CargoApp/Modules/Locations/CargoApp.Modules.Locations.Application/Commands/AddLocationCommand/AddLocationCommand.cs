using CargoApp.Modules.Locations.Application.DTO;
using MediatR;
using Result;
using Result.ApiResult;

namespace CargoApp.Modules.Locations.Application.Commands.AddLocationCommand;

public record AddLocationCommand(LocationDto Location) : IRequest<ApiResult<string>>;