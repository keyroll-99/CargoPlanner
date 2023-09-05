using CargoApp.Core.Infrastructure.Policies;
using CargoApp.Core.Infrastructure.Response;
using CargoApp.Core.ShareCore.Clock;
using CargoApp.Core.ShareCore.Entites;
using CargoApp.Core.ShareCore.Policies;
using CargoApp.Modules.Companies.Core.Commands.CreateCompany;
using Microsoft.AspNetCore.Http;

namespace CargoApp.Modules.Companies.Core.Entities;

public class Company : BaseEntity
{
    public string Name { get; private set; }
    public CompanyType CompanyType { get; set; }
    public List<Worker> Workers { get; set; } = new();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    // Empty constructor for EF
    public Company()
    {
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Company(Guid id, DateTime createAt, string name, CompanyType companyType) : base(id, createAt)
    {
        Name = name;
        CompanyType = companyType;
    }

    public static async Task<Result<Company, string>> CreateFromCommand(CreateCompanyCommand command,
        IEnumerable<IPolicy<CreateCompanyCommand>> policies, IClock clock)
    {
        var result = await policies.UsePolicies(command);

        var (success, error, isSuccess) = await result.Match<Company, string>(
            () => Task.FromResult(new Company(Guid.NewGuid(), clock.Now(), command.Name, command.CompanyType)),
            Task.FromResult);

        return isSuccess
            ? Result<Company, string>.Success(success, StatusCodes.Status201Created)
            : Result<Company, string>.Fail(error, result.StatusCode);
    }
}