namespace FantasyInformator.Shared.Mappers;

/// <summary>
/// Code that uses the mappers must be able to tell a mapping error apart from any other error, and see what went
/// wrong without debugging. So every error the mappers find themselves is thrown as this exception. It holds the class
/// of the original, the class of the copy when that is known, and the property when one property is the cause.
/// Example: copying a TeamDb into a Team, when Team has a property Players and TeamDb has none, throws this with
/// TeamDb, Team and Players.
/// </summary>
public sealed class MappingException : Exception
{
    /// <summary>Makes the exception without a message or classes.</summary>
    public MappingException()
    {
    }

    /// <summary>Makes the exception with only a message.</summary>
    public MappingException(string message)
        : base(message)
    {
    }

    /// <summary>Makes the exception with only a message and its cause.</summary>
    public MappingException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>Makes the exception with the classes.</summary>
    public MappingException(string message, Type sourceType, Type? targetType)
        : this(message, sourceType, targetType, null, null)
    {
    }

    /// <summary>Makes the exception with the classes and the property that is the cause.</summary>
    public MappingException(string message, Type sourceType, Type? targetType, string propertyName)
        : this(message, sourceType, targetType, propertyName, null)
    {
    }

    /// <summary>
    /// Makes the exception with the classes, the property when one property is the cause, and the cause.
    /// </summary>
    public MappingException(
        string message,
        Type sourceType,
        Type? targetType,
        string? propertyName,
        Exception? innerException)
        : base(message, innerException)
    {
        SourceType = sourceType;
        TargetType = targetType;
        PropertyName = propertyName;
    }

    /// <summary>The class of the original, or null when it was not given.</summary>
    public Type? SourceType { get; }

    /// <summary>The class of the copy, or null when the mapper does not know it.</summary>
    public Type? TargetType { get; }

    /// <summary>The property that caused the error, or null when no single property did.</summary>
    public string? PropertyName { get; }
}
