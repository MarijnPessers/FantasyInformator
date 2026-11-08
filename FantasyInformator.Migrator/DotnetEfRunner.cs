using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using FantasyInformator.Migrator.Resources;
using FantasyInformator.Migrator.Settings;

namespace FantasyInformator.Migrator;

/// <summary>The exit code and the combined text output of one <c>dotnet ef</c> run.</summary>
public record DotnetEfResult(int ExitCode, string Output);

/// <summary>
/// Runs the <c>dotnet ef</c> command line tool against the Database project, with the startup project that registers
/// the context. Migrations are created through the tool, because it builds the project first, so the new migration
/// files match the current models.
/// </summary>
public class DotnetEfRunner(
    string databaseProjectPath,
    string startupProjectPath,
    string connectionString)
{
    public const string AddMigrationCommand = "add";
    public const string HasPendingModelChangesCommand = "has-pending-model-changes";

    private const string DotnetProgram = "dotnet";
    private const string DotnetWindowsFileName = "dotnet.exe";

    // The runtime folder is <dotnet>/shared/Microsoft.NETCore.App/<version>, so the dotnet program is three folders up.
    private const string RuntimeToDotnetFolder = "../../..";

    private const string EfTool = "ef";
    private const string MigrationsGroup = "migrations";
    private const string DatabaseGroup = "database";
    private const string UpdateCommand = "update";
    private const string ProjectOption = "--project";
    private const string StartupProjectOption = "--startup-project";

    // The Migrator already holds the connection string in its own user secrets, so the tool gets it here instead of
    // needing the same secret in the Database project.
    private const string ConnectionOption = "--connection";

    private static readonly string DatabaseProjectSetting =
        $"{MigratorSettings.SectionName}:{nameof(MigratorSettings.DatabaseProjectPath)}";

    private static readonly string StartupProjectSetting =
        $"{MigratorSettings.SectionName}:{nameof(MigratorSettings.StartupProjectPath)}";

    private static readonly CompositeFormat FolderNotFoundFormat =
        CompositeFormat.Parse(Texts.ProjectFolderNotFound);

    public Task<DotnetEfResult> RunMigrationsCommandAsync(string command, params string[] commandArguments) =>
        RunAsync([MigrationsGroup, command, .. commandArguments]);

    /// <summary>Applies migrations up to the target, or all of them when no target is given.</summary>
    public Task<DotnetEfResult> RunDatabaseUpdateAsync(string? targetMigrationId) =>
        targetMigrationId is null
            ? RunAsync([DatabaseGroup, UpdateCommand, ConnectionOption, connectionString])
            : RunAsync([DatabaseGroup, UpdateCommand, targetMigrationId, ConnectionOption, connectionString]);

    private async Task<DotnetEfResult> RunAsync(string[] efArguments)
    {
        var startInfo = new ProcessStartInfo(ResolveDotnetPath())
        {
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false
        };

        startInfo.ArgumentList.Add(EfTool);

        foreach (var efArgument in efArguments)
        {
            startInfo.ArgumentList.Add(efArgument);
        }

        startInfo.ArgumentList.Add(ProjectOption);
        startInfo.ArgumentList.Add(ResolveDatabaseProjectPath());
        startInfo.ArgumentList.Add(StartupProjectOption);
        startInfo.ArgumentList.Add(ResolveProjectPath(startupProjectPath, StartupProjectSetting));

        using var process = Process.Start(startInfo)
            ?? throw new InvalidOperationException(Texts.DotnetNotStarted);

        // Both streams are read at the same time, because the tool blocks when one of them is full and nobody reads it.
        var outputTask = process.StandardOutput.ReadToEndAsync();
        var errorTask = process.StandardError.ReadToEndAsync();

        await process.WaitForExitAsync();

        var output = await outputTask + await errorTask;

        return new DotnetEfResult(process.ExitCode, output.Trim());
    }

    public string ResolveDatabaseProjectPath() =>
        ResolveProjectPath(databaseProjectPath, DatabaseProjectSetting);

    private static string ResolveProjectPath(string projectPath, string settingName)
    {
        if (Path.IsPathRooted(projectPath))
        {
            return projectPath;
        }

        // The working folder depends on how the program is started (the solution folder, the project folder or a bin
        // folder), so a relative path is looked up from the folder of the program upwards.
        for (var folder = new DirectoryInfo(AppContext.BaseDirectory); folder is not null; folder = folder.Parent)
        {
            var candidate = Path.Combine(folder.FullName, projectPath);

            if (Directory.Exists(candidate))
            {
                return candidate;
            }
        }

        throw new DirectoryNotFoundException(
            string.Format(
                CultureInfo.CurrentCulture,
                FolderNotFoundFormat,
                projectPath,
                AppContext.BaseDirectory,
                settingName));
    }

    private static string ResolveDotnetPath()
    {
        var dotnetFolder = Path.GetFullPath(
            Path.Combine(RuntimeEnvironment.GetRuntimeDirectory(), RuntimeToDotnetFolder));
        var fileName = OperatingSystem.IsWindows()
            ? DotnetWindowsFileName
            : DotnetProgram;

        return Path.Combine(dotnetFolder, fileName);
    }
}
