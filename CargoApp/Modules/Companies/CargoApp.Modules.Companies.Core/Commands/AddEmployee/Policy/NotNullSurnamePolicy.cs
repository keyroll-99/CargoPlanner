using CargoApp.Core.ShareCore.Policies;
using CargoApp.Modules.Companies.Core.Commands.AddEmployee;
using Microsoft.AspNetCore.Http;

namespace CargoApp.Modules.Companies.Core.Commands.AddWorker.Policy;

internal class NotNullSurnamePolicy : IPolicy<CreateEmployeeCommand>
{
    public string ErrorMessage => "Invalid surname";
    public int StatusCode => StatusCodes.Status400BadRequest;

    public bool IsApplicable(CreateEmployeeCommand model)
        => true;

    public ValueTask<bool> IsValidAsync(CreateEmployeeCommand model)
        =>
            ValueTask.FromResult<bool>(!string.IsNullOrWhiteSpace(model.Surname));
}