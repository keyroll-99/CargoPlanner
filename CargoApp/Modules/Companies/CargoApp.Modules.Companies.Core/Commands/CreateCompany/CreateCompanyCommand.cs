using CargoApp.Modules.Companies.Core.Entities;
using MediatR;
using Result.ApiResult;

namespace CargoApp.Modules.Companies.Core.Commands.CreateCompany;

public record CreateCompanyCommand(string Name, CompanyType CompanyType) : IRequest<ApiResult<string>>;