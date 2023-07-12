namespace Planner.Service.Time;

public class Clock : IClock
{
    public DateTime Now()
        => DateTime.Now;
}