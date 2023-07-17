namespace CargoApp.Core.Infrastructure.Entites;

public class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreateAt { get; set; }
}