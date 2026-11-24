namespace FantasyInformator.Tool.MapGenerator.Tests.Models;

public class ModelBase
{
    public int Id { get; init; }
}

public class Player : ModelBase
{
    public required string Name { get; init; }
}

public abstract class AbstractModel : ModelBase
{
    public abstract string Name { get; }
}
