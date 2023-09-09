using System.Text.RegularExpressions;
using CargoApp.Core.ShareCore.Policies;
using Microsoft.AspNetCore.Http;

namespace CargoApp.Modules.Companies.Core.Commands.AddWorker.Policy;

internal partial class ValidWorkerEmailPolicy : IPolicy<CreateEmployeeCommand>
{
    public string ErrorMessage => "Invalid worker email";
    public int StatusCode => StatusCodes.Status400BadRequest;

    public bool IsApplicable(CreateEmployeeCommand model)
        => true;

    public ValueTask<bool> IsValidAsync(CreateEmployeeCommand model)
        =>
            ValueTask.FromResult<bool>(!string.IsNullOrWhiteSpace(model.Email) && MailRegex().IsMatch(model.Email));
    
    
    [GeneratedRegex(
        "^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$")]
    private static partial Regex MailRegex();
}