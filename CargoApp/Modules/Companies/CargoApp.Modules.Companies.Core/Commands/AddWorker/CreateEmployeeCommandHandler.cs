using CargoApp.Core.Abstraction.Context;
using CargoApp.Core.Infrastructure.Policies;
using CargoApp.Core.Infrastructure.Response;
using CargoApp.Core.ShareCore.Clock;
using CargoApp.Core.ShareCore.Policies;
using CargoApp.Modules.Companies.Core.Entities;
using CargoApp.Modules.Companies.Core.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CargoApp.Modules.Companies.Core.Commands.AddWorker;

internal class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Result<string>>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEnumerable<IPolicy<CreateEmployeeCommand>> _policies;
    private readonly IClock _clock;
    private readonly IContext _context;

    public CreateEmployeeCommandHandler(
        ICompanyRepository companyRepository,
        IEmployeeRepository employeeRepository,
        IEnumerable<IPolicy<CreateEmployeeCommand>> policies,
        IClock clock, IContext context)
    {
        _companyRepository = companyRepository;
        _employeeRepository = employeeRepository;
        _policies = policies;
        _clock = clock;
        _context = context;
    }

    public async Task<Result<string>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var policyResult = await _policies.UsePolicies(request);
        if (!policyResult.IsSuccess)
        {
            return Result<string>.Fail(policyResult.Error, policyResult.StatusCode);
        }

        var company = await _companyRepository.GetByIdAsync(request.CompanyId != Guid.Empty
            ? request.CompanyId
            : _context.IdentityContext.CompanyId);

        if (company is null)
        {
            return Result<string>.Fail("Company not found");
        }

        var worker = new Employee(
            Guid.NewGuid(),
            _clock.Now(),
            request.Name,
            request.Surname,
            request.Email,
            request.Position,
            company.Id,
            company
        );

        await _employeeRepository.AddAsync(worker);

        // TODO publish rabbit event
        
        return Result<string>.Success(worker.Id.ToString(), StatusCodes.Status201Created);
    }
}