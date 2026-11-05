using System.ComponentModel.DataAnnotations;

namespace FantasyInformator.Database;

/// <summary>
/// The settings the Database project needs, read from the <see cref="SectionName"/> section.
/// </summary>
public class DatabaseOptions
{
    public const string SectionName = "Database";

    /// <summary>The connection string of the user that reads and writes data while the application runs. Required.</summary>
    [Required]
    public string ConnectionStringApp { get; set; } = string.Empty;

    /// <summary>The connection string of the user that creates and changes tables. Used for migrations.</summary>
    public string ConnectionStringEF { get; set; } = string.Empty;

    /// <summary>The folder where saves that failed are queued as files.</summary>
    public string FailedSaveQueueFolder { get; set; } = string.Empty;
}
