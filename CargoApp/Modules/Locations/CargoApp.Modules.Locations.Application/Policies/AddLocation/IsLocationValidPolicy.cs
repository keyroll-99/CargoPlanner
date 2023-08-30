using CargoApp.Core.ShareCore.Policies;
using CargoApp.Modules.Locations.Application.Commands.AddLocationCommand;
using CargoApp.Modules.Locations.Application.DTO;
using Microsoft.AspNetCore.Http;

namespace CargoApp.Modules.Locations.Application.Policies.AddLocation;

internal class IsLocationValidPolicy : IPolicy<AddLocationCommand>
{
    public string ErrorMessage => "Cannot add location";
    public int StatusCode => StatusCodes.Status400BadRequest;

    public bool IsApplicable(AddLocationCommand model)
        => true;

    public ValueTask<bool> IsValidAsync(AddLocationCommand model)
    {
        var location = model.Location;

        return new ValueTask<bool>(IsLocationValid(model.Location));
    }

    private static bool IsLocationValid(LocationDto location)
        => !string.IsNullOrWhiteSpace(location.Name) &&
           !string.IsNullOrWhiteSpace(location.DisplayName) &&
           location.OsmId > 0
           && IsAddressValid(location.Address);

    private static bool IsAddressValid(AddressDto address)
        => !string.IsNullOrWhiteSpace(address.Country);
}