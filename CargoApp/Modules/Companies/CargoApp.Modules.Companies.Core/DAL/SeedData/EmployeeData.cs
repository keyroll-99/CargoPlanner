using CargoApp.Core.Infrastructure.Clock;
using CargoApp.Core.ShareCore.Clock;
using CargoApp.Modules.Companies.Core.Entities;

namespace CargoApp.Modules.Companies.Core.DAL.SeedData;

internal class EmployeeData
{
    private static readonly IClock Clock = new Clock();

    public static readonly IList<Employee> Employees = new List<Employee>
    {
        new Employee(
            Guid.Parse("49ec2f72-be39-44ca-9f5d-d6816c305413"),
            Clock.Now(),
            "Admin",
            "Admin",
            "admin@admin.com",
            WorkingPositionEnum.Owner | WorkingPositionEnum.Dispatcher | WorkingPositionEnum.Driver,
            Guid.Parse("d81e5308-93c6-4ba8-817d-f6b69e9ae703"),
            null
        ),
        new Employee(
            Guid.Parse("3ee63160-4399-42d5-b99a-6d9f054aa53f"),
            Clock.Now(),
            "John",
            "Doe",
            "john.doe@cargoapp.com",
            WorkingPositionEnum.Driver,
            Guid.Parse("d81e5308-93c6-4ba8-817d-f6b69e9ae703"),
            null
        ),
        new Employee(
            Guid.Parse("d1e6b8a9-9b9e-4b0e-8b9a-9b9e4b0e8b9a"),
            Clock.Now(),
            "Mark",
            "Doe",
            "mark.doe@cargoapp.com",
            WorkingPositionEnum.Driver,
            Guid.Parse("d81e5308-93c6-4ba8-817d-f6b69e9ae703"),
            null
        ),
        new Employee(
            Guid.Parse("bc161207-136d-4910-91bb-8413749a9282"),
            Clock.Now(),
            "dispatcher",
            "dispatcher",
            "dispatcher@cargoapp.com",
            WorkingPositionEnum.Dispatcher,
            Guid.Parse("d81e5308-93c6-4ba8-817d-f6b69e9ae703"),
            null
        )
    };
}