﻿using CargoApp.Core.ShareCore.Clock;

namespace CargoApp.Core.Infrastructure.Clock;

public class Clock : IClock
{
    public DateTime Now()
    {
        return DateTime.UtcNow;
    }
}