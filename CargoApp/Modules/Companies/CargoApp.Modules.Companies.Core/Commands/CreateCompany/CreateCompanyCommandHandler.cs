﻿using CargoApp.Core.Abstraction.QueueMessages;
using CargoApp.Core.Infrastructure.Policies;
using CargoApp.Core.ShareCore.Clock;
using CargoApp.Core.ShareCore.Policies;
using CargoApp.Modules.Companies.Core.Entities;
using CargoApp.Modules.Companies.Core.Repositories;
using CargoApp.Modules.Contracts.Events.Companies;
using MediatR;
using Microsoft.AspNetCore.Http;
using Result.ApiResult;

namespace CargoApp.Modules.Companies.Core.Commands.CreateCompany;

internal class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, ApiResult<string>>
{
    private readonly IEnumerable<IPolicy<CreateCompanyCommand>> _policies;
    private readonly ICompanyRepository _companyRepository;
    private readonly IClock _clock;
    private readonly IEventManager _eventManager;

    public CreateCompanyCommandHandler(
        IEnumerable<IPolicy<CreateCompanyCommand>> policies,
        ICompanyRepository companyRepository,
        IClock clock,
        IEventManager eventManager)
    {
        _policies = policies;
        _companyRepository = companyRepository;
        _clock = clock;
        _eventManager = eventManager;
    }

    public async Task<ApiResult<string>> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        var policyResult = await _policies.UsePolicies(request);
        if (!policyResult.IsSuccess)
        {
            return ApiResult<string>.Fail(policyResult.ErrorMessage, policyResult.StatusCode);
        }

        var company = new Company(Guid.NewGuid(), _clock.Now(), request.Name, request.CompanyType);
        await _companyRepository.AddAsync(company);
        
        _eventManager.PublishEvent(new CompanyCreateEvent(company.Id, company.Name));

        return ApiResult<string>.Success(company.Id.ToString(), StatusCodes.Status201Created);
    }
}