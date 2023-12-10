using CargoApp.Core.Abstraction.Context;
using CargoApp.Core.Abstraction.QueueMessages;
using CargoApp.Core.Infrastructure.Policies;
using CargoApp.Core.ShareCore.Policies;
using CargoApp.Modules.Contracts.Events.Locations;
using CargoApp.Modules.Locations.Application.Mappers.Location;
using CargoApp.Modules.Locations.Application.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Result;
using Result.ApiResult;

namespace CargoApp.Modules.Locations.Application.Commands.AddLocationCommand;

internal class AddLocationCommandHandler : IRequestHandler<AddLocationCommand, ApiResult<string>>
{
    private readonly ILocationRepository _locationRepository;
    private readonly IEnumerable<IPolicy<AddLocationCommand>> _policies;
    private readonly IContext _context;
    private readonly IEventManager _eventManager;

    public AddLocationCommandHandler(
        ILocationRepository locationRepository,
        IEnumerable<IPolicy<AddLocationCommand>> policies,
        IContext context,
        IEventManager eventManager)
    {
        _locationRepository = locationRepository;
        _policies = policies;
        _context = context;
        _eventManager = eventManager;
    }

    public async Task<ApiResult<string>> Handle(AddLocationCommand command, CancellationToken cancellationToken)
    {
        var commandLocation = command.Location;
        var existsLocation = await _locationRepository.GetByOsmIdAndCompanyIdAsync(commandLocation.OsmId, _context.IdentityContext.CompanyId);
        if (existsLocation is not null)
        {
            return ApiResult<string>.Success(existsLocation.Id.ToString());
        }

        var policyResult = await _policies.UsePolicies(command);
        var (successResult, errorResult) = await policyResult.MatchAsync<ApiResult<string>, ApiResult<string>>(
            async (_) =>
            {
                var location = command.Location.AsEntity(_context.IdentityContext.CompanyId);
                var result = await _locationRepository.AddAsync(location);
                _eventManager.PublishEvent(new LocationCreatedEvent(result.Lat, result.Lon, result.DisplayName, result.OsmId));
                return ApiResult<string>.Success(result.Id.ToString(), StatusCodes.Status201Created);
            }, (error) => Task.FromResult(ApiResult<string>.Fail(error)));

        return successResult ?? errorResult!;
    }
}