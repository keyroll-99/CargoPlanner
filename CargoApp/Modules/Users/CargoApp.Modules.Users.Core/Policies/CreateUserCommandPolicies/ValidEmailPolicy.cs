using System.Text.RegularExpressions;
using CargoApp.Core.ShareCore.Policies;
using CargoApp.Modules.Users.Core.Commands;
using Microsoft.AspNetCore.Http;

namespace CargoApp.Modules.Users.Core.Policies.CreateUserCommandPolicies;

internal sealed partial class ValidEmailPolicy : IPolicy<CreateUserCommand>
{
    public string ErrorMessage => "Invalid Email.";
    public int StatusCode => StatusCodes.Status400BadRequest;


    public bool IsApplicable(CreateUserCommand model)
    {
        return true;
    }

    public ValueTask<bool> IsValidAsync(CreateUserCommand model)
    {
        return ValueTask.FromResult(MailRegex().IsMatch(model.Email));
    }

    [GeneratedRegex(
        "^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$")]
    private static partial Regex MailRegex();
}