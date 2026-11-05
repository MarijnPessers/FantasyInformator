using System.ComponentModel.DataAnnotations;

namespace FantasyInformator.Sleeper;

/// <summary>
/// The settings the Sleeper project needs, read from the <see cref="SectionName"/> section.
/// </summary>
public class SleeperOptions
{
    public const string SectionName = "Sleeper";

    /// <summary>The Sleeper username whose leagues are retrieved. Required.</summary>
    [Required]
    public string Username { get; set; } = string.Empty;
}
