using CargoApp.Modules.Companies.Core.Entities;
using MediatR;
using Result;
using Result.ApiResult;

namespace CargoApp.Modules.Companies.Core.Commands.AddEmployee;

public record CreateEmployeeCommand(string Name, string Surname, string Email, WorkingPositionEnum Position, Guid CompanyId) : IRequest<ApiResult<string>>{}