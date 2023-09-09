using CargoApp.Core.Infrastructure.Policies;
using CargoApp.Core.ShareCore.Clock;
using CargoApp.Core.ShareCore.Entites;
using CargoApp.Core.ShareCore.Policies;
using CargoApp.Modules.Companies.Core.Commands.CreateCompany;
using Microsoft.AspNetCore.Http;

namespace CargoApp.Modules.Companies.Core.Entities;

public class Company : BaseEntity
{
    public string Name { get; private set; }
    public CompanyType CompanyType { get; private set; }
    public List<Employee> Employees { get; private set; } = new();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Company()
    {
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Company(Guid id, DateTime createAt, string name, CompanyType companyType) : base(id, createAt)
    {
        Name = name;
        CompanyType = companyType;
    }
}