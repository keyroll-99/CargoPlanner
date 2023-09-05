using CargoApp.Core.ShareCore.Entites;

namespace CargoApp.Modules.Companies.Core.Entities;

public class Worker : BaseEntity
{
    public Guid UserId { get; set; }
    public WorkingPositionEnum WorkingPosition { get; set; }
    public Guid CompanyId { get; set; }
    public Company Company { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
// Empty constructor for EF
    public Worker()
    {
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Worker(Guid id, DateTime createAt, Guid userId, WorkingPositionEnum workingPosition, Guid companyId, Company company) : base(id, createAt)
    {
        UserId = userId;
        WorkingPosition = workingPosition;
        CompanyId = companyId;
        Company = company;
    }
}