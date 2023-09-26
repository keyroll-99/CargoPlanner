using CargoApp.Core.ShareCore.Entites;

namespace CargoApp.Modules.Companies.Core.Entities;

public class Employee : BaseEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public WorkingPositionEnum WorkingPosition { get; set; }
    public bool IsActive { get; set; }
    public Guid CompanyId { get; set; }
    public Company Company { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
// Empty constructor for EF
    public Employee()
    {
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Employee(Guid id, DateTime createAt, string name, string surname, string email, WorkingPositionEnum workingPosition, Guid companyId, Company company) : base(id, createAt)
    {
        Name = name;
        Surname = surname;
        Email = email;
        WorkingPosition = workingPosition;
        CompanyId = companyId;
        Company = company;
    }
}