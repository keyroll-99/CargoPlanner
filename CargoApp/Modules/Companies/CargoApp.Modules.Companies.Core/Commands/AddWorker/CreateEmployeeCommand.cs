using CargoApp.Core.Infrastructure.Response;
using CargoApp.Modules.Companies.Core.Entities;
using MediatR;

namespace CargoApp.Modules.Companies.Core.Commands.AddWorker;

public record CreateEmployeeCommand(string Name, string Surname, string Email, WorkingPositionEnum Position, Guid CompanyId) : IRequest<Result<string>>{}