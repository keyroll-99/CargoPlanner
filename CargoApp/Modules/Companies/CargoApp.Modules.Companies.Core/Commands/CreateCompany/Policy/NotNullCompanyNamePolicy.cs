using CargoApp.Core.ShareCore.Policies;
using Microsoft.AspNetCore.Http;

namespace CargoApp.Modules.Companies.Core.Commands.CreateCompany.Policy;

internal class NotNullCompanyNamePolicy : IPolicy<CreateCompanyCommand>
{
    public string ErrorMessage => "Company name cannot be null";
    public int StatusCode => StatusCodes.Status400BadRequest;

    public bool IsApplicable(CreateCompanyCommand model)
        => true;

    public ValueTask<bool> IsValidAsync(CreateCompanyCommand model)
        => ValueTask.FromResult(!string.IsNullOrWhiteSpace(model.Name));
}