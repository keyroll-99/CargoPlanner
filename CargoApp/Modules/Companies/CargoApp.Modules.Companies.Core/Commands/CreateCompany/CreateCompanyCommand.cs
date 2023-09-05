using CargoApp.Core.Infrastructure.Response;
using CargoApp.Modules.Companies.Core.Entities;
using MediatR;

namespace CargoApp.Modules.Companies.Core.Commands.CreateCompany;

public record CreateCompanyCommand(string Name, CompanyType CompanyType) : IRequest<Result<string>>;