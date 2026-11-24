namespace FantasyInformator.Tool.MapGenerator.Tests.Models;

public class ModelBase;

public class Player : ModelBase;

public abstract class AbstractModel : ModelBase
{
    public abstract string Name { get; }
}
