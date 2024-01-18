namespace CargoApp.Modules.Planner.Core.Planner;

public class Cargo
{
    public required Guid Id { get; init; }
    public required Location From { get; init; }
    public required Location To { get; init; }
    public required DateTime ExpectedDeliveryTime { get; init; }
    public Driver? Driver { get; set; }
    
}