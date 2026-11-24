namespace FantasyInformator.Tool.MapGenerator;

public sealed class MapGeneratorException : Exception
{
    /// <summary>Makes the exception without a message.</summary>
    public MapGeneratorException()
    {
    }

    /// <summary>Makes the exception with a message.</summary>
    public MapGeneratorException(string message)
        : base(message)
    {
    }

    /// <summary>Makes the exception with a message and its cause.</summary>
    public MapGeneratorException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
