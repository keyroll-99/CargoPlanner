namespace CargoApp.Modules.Cargoes.Core.CompanyAggregate.ValueObject;

public class CompanyName
{
    public string Name { get; private set; }

    public CompanyName(string name)
    {
        Name = name;
    }

    public static implicit operator string(CompanyName companyName) => companyName.Name;

    public static implicit operator CompanyName(string name) => new CompanyName(name);

}