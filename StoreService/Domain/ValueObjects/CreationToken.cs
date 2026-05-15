namespace StoreService.Api.Domain.ValueObjects;

public record CreationToken(Guid Value)
{
    public static CreationToken New() => new(Guid.NewGuid());
    
    public static CreationToken From(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("CreationToken cannot be empty.");

        return new CreationToken(value);
    }

    public override string ToString() => Value.ToString();
}