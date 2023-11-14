using CargoApp.Modules.Locations.Application.DTO;
using MediatR;
using CargoApp.Core.Infrastructure.Response;

namespace CargoApp.Modules.Locations.Application.Commands.AddLocationCommand;

public record AddLocationCommand(LocationDto Location) : IRequest<Result<string>>;