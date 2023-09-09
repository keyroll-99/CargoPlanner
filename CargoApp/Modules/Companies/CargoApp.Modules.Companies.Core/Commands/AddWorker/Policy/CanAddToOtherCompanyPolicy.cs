using CargoApp.Core.Abstraction.Context;
using CargoApp.Core.Infrastructure.Context;
using CargoApp.Core.ShareCore.Enums;
using CargoApp.Core.ShareCore.Policies;
using Microsoft.AspNetCore.Http;

namespace CargoApp.Modules.Companies.Core.Commands.AddWorker.Policy;

public class CanAddToOtherCompanyPolicy : IPolicy<CreateEmployeeCommand>
{
    private readonly IContext _context;

    public CanAddToOtherCompanyPolicy(IContext context)
    {
        _context = context;
    }

    public string ErrorMessage => "Cannot add worker to other company";
    public int StatusCode => StatusCodes.Status400BadRequest;


    public bool IsApplicable(CreateEmployeeCommand model)
        => !_context.IdentityContext.HasPermission(PermissionEnum.Admin);

    public ValueTask<bool> IsValidAsync(CreateEmployeeCommand model)
        =>
            ValueTask.FromResult<bool>(model.CompanyId == Guid.Empty);
}