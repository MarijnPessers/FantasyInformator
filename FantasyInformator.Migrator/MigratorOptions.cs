namespace FantasyInformator.Migrator;

/// <summary>
/// The settings of the Migrator, read from the <see cref="SectionName"/> section.
/// </summary>
public class MigratorOptions
{
    public const string SectionName = "Migrator";

    /// <summary>
    /// The folder of the Database project, where <c>dotnet ef</c> creates the migrations. A relative path is looked
    /// up from the folder of the program upwards, so it works from the solution folder and from Visual Studio.
    /// </summary>
    public string DatabaseProjectPath { get; set; } = string.Empty;
}
