using CargoApp.Core.Infrastructure.CQRS.Request;
using CargoApp.Core.Infrastructure.Response;
using CargoApp.Modules.Locations.Application.DTO;
using MediatR;


namespace CargoApp.Modules.Locations.Application.Commands.AddLocationCommand;

public record AddLocationCommand(LocationDto Location) : IRequest<Result<string>>;