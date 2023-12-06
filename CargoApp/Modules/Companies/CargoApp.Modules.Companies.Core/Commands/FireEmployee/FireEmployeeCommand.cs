using MediatR;
using Result.ApiResult;

namespace CargoApp.Modules.Companies.Core.Commands.FireEmployee;

public record FireEmployeeCommand(Guid EmployeeId): IRequest<ApiResult>;