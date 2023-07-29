namespace CargoApp.Core.Infrastructure.Entites;

public class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreateAt { get; set; }

    public BaseEntity()
    {
    }

    public BaseEntity(Guid id, DateTime createAt)
    {
        Id = id;
        CreateAt = createAt;
    }
}