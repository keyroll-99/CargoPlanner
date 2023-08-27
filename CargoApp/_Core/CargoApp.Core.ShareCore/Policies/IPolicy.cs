namespace CargoApp.Core.ShareCore.Policies;

public interface IPolicy<in T> : IPolicyMarker
{
    public string ErrorMessage { get; }
    public int StatusCode { get; }
    public bool IsApplicable(T model);
    public ValueTask<bool> IsValidAsync(T model);
}