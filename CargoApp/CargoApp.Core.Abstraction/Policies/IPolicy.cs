﻿namespace CargoApp.Core.Abstraction.Policies;

public interface IPolicy<T>
{
    public string ErrorMessage { get; }
    public bool CanBeApplied(T model);
    public ValueTask<bool> IsValid(T model);
}