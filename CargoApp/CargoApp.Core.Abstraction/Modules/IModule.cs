namespace CargoApp.Core.Abstraction.Modules;

public interface IModule
{
    public string Name { get; }
    public string Path { get; }

    public void LoadModule();
    public void UseModule();
}