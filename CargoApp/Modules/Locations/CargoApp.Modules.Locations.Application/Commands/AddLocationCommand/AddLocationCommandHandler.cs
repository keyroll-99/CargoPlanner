using CargoApp.Core.Abstraction.Context;
using CargoApp.Core.Infrastructure.Policies;
using CargoApp.Core.Infrastructure.Response;
using CargoApp.Core.ShareCore.Policies;
using CargoApp.Modules.Locations.Application.Mappers.Location;
using CargoApp.Modules.Locations.Application.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CargoApp.Modules.Locations.Application.Commands.AddLocationCommand;

internal class AddLocationCommandHandler : IRequestHandler<AddLocationCommand, Result<string>>
{
    private readonly ILocationRepository _locationRepository;
    private readonly IEnumerable<IPolicy<AddLocationCommand>> _policies;
    private readonly IContext _context;

    public AddLocationCommandHandler(
        ILocationRepository locationRepository,
        IEnumerable<IPolicy<AddLocationCommand>> policies,
        IContext context)
    {
        _locationRepository = locationRepository;
        _policies = policies;
        _context = context;
    }

    public async Task<Result<string>> Handle(AddLocationCommand command, CancellationToken cancellationToken)
    {
        var commandLocation = command.Location;
        var existsLocation = await _locationRepository.GetByOsmIdAndCompanyIdAsync(commandLocation.OsmId, _context.IdentityContext.CompanyId);
        if (existsLocation is not null)
        {
            return Result<string>.Success(existsLocation.Id.ToString());
        }

        var policyResult = await _policies.UsePolicies(command);
        return await policyResult.Match<Result<string>>(
            async () =>
            {
                var location = command.Location.AsEntity(_context.IdentityContext.CompanyId);
                var result = await _locationRepository.AddAsync(location);
                return Result<string>.Success(result.Id.ToString(), StatusCodes.Status201Created);
            }, (error) => Task.FromResult(Result<string>.Fail(error)));
    }
}