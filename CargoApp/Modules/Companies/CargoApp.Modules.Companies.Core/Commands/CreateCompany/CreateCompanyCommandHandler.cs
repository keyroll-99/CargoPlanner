using CargoApp.Core.Infrastructure.Response;
using CargoApp.Core.ShareCore.Clock;
using CargoApp.Core.ShareCore.Policies;
using CargoApp.Modules.Companies.Core.Entities;
using CargoApp.Modules.Companies.Core.Repositories;
using MediatR;

namespace CargoApp.Modules.Companies.Core.Commands.CreateCompany;

internal class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, Result<string>>
{
    private readonly IEnumerable<IPolicy<CreateCompanyCommand>> _policies;
    private readonly ICompanyRepository _companyRepository;
    private readonly IClock _clock;

    public CreateCompanyCommandHandler(IEnumerable<IPolicy<CreateCompanyCommand>> policies, ICompanyRepository companyRepository, IClock clock)
    {
        _policies = policies;
        _companyRepository = companyRepository;
        _clock = clock;
    }

    public async Task<Result<string>> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        var createCompanyResult = await Company.CreateFromCommand(request, _policies, _clock);

        await createCompanyResult.MatchOnlySuccessAsync(async company => await _companyRepository.CreateAsync(company));

        if (createCompanyResult.IsSuccess)
        {
            return Result<string>.Success(createCompanyResult.SuccessModel.Id.ToString(),
                createCompanyResult.StatusCode);
        }

        return Result<string>.Fail(createCompanyResult.Error, createCompanyResult.StatusCode);
    }
}