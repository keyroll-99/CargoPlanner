using CargoApp.Core.Abstraction.Clock;

namespace CargoApp.Core.Infrastructure.Clock;

public class Clock : IClock
{
    public DateTime Now()
     => DateTime.UtcNow;
}