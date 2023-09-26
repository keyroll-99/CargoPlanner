using CargoApp.Core.Infrastructure.Response;
using MediatR;

namespace CargoApp.Modules.Companies.Core.Commands.FireEmployee;

public record FireEmployeeCommand(Guid EmployeeId): IRequest<Result>;