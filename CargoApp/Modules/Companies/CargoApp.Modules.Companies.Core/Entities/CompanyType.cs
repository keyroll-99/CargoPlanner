namespace CargoApp.Modules.Companies.Core.Entities;

[Flags]
public enum CompanyType
{
    Delivery = 1 << 0,
    Ordering = 1 << 1
}