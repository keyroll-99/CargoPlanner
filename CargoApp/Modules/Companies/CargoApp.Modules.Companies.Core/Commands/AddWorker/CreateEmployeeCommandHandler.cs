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
    private readonly IWorkerRepository _workerRepository;
    private readonly IEnumerable<IPolicy<CreateEmployeeCommand>> _policies;
    private readonly IClock _clock;
    private readonly IContext _context;

    public CreateEmployeeCommandHandler(
        ICompanyRepository companyRepository,
        IWorkerRepository workerRepository,
        IEnumerable<IPolicy<CreateEmployeeCommand>> policies,
        IClock clock, IContext context)
    {
        _companyRepository = companyRepository;
        _workerRepository = workerRepository;
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

        await _workerRepository.AddAsync(worker);

        // TODO publish rabbit event
        
        return Result<string>.Success(worker.Id.ToString(), StatusCodes.Status201Created);
    }
}