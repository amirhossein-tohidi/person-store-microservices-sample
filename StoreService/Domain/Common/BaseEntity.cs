namespace StoreService.Api.Domain.Common;

public abstract class BaseEntity: IEntity
{
    public long Id { get; protected set; } 

    protected BaseEntity() { }

    protected BaseEntity(long id)
    {
        Id = id;
    }
}