namespace CargoApp.Core.Abstraction.Policies;

public interface IPolicy<T>
{
    public bool CanBeApplied(T model);
    public bool IsValid(T model);
}