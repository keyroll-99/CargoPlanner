using CargoApp.Core.ShareCore.Policies;
using Microsoft.AspNetCore.Http;

namespace CargoApp.Modules.Companies.Core.Commands.AddWorker.Policy;

internal class NotNullWorkerNamePolicy : IPolicy<CreateEmployeeCommand>
{
    public string ErrorMessage => "Invalid name";
    public int StatusCode => StatusCodes.Status400BadRequest;

    public bool IsApplicable(CreateEmployeeCommand model)
        => true;

    public ValueTask<bool> IsValidAsync(CreateEmployeeCommand model)
        => ValueTask.FromResult(!string.IsNullOrWhiteSpace(model.Name));
}