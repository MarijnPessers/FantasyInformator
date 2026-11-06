using System.Globalization;
using FantasyInformator.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FantasyInformator.Migrator;

/// <summary>
/// The console menu: create a migration, apply all migrations, or move the database to a chosen migration.
/// </summary>
public sealed class MigratorMenu(FantasyInformatorDbContext context, DotnetEfRunner dotnetEfRunner)
{
    // The ids of migrations start with the UTC time they were created, for example 20261105003433_InitialCreate.
    private const string MigrationIdTimestampFormat = "yyyyMMddHHmmss";
    private const char MigrationIdNameSeparator = '_';
    private const string ListedDateFormat = "yyyy-MM-dd HH:mm";

    // Part of the text that the has-pending-model-changes command prints when the model has changed.
    private const string ModelChangedText = "Changes have been made to the model";

    private const string ConfirmRollbackAnswer = "yes";

    private const int PageSize = 9;
    private const string BackToMenuAnswer = "0";
    private const string PreviousPageAnswer = "p";
    private const string NextPageAnswer = "n";

    private const string EmptyDatabaseText = "Empty database (before the first migration)";

    private const string MigrationsFolderName = "Migrations";
    private const string MigrationFilePattern = "*.cs";
    private const string MigrationFileExtension = ".cs";
    private const string DesignerFileSuffix = ".Designer";

    private enum MigrationStatus
    {
        Applied,
        Pending,
        NotBuiltYet
    }

    // The empty database is listed as a state too. It uses the id that EF has for "before the first migration".
    private sealed record ListedMigration(string Id, MigrationStatus Status)
    {
        public bool IsEmptyDatabase => Id == Migration.InitialDatabase;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            PrintMenu();

            var choice = ReadMenuChoice();

            if (choice is null)
            {
                Console.WriteLine("That is not one of the options.");
                continue;
            }

            if (choice == MenuChoice.Exit)
            {
                return;
            }

            // The program must stay open after a failure, so the developer sees the error and can choose again.
            try
            {
                await RunChoiceAsync(choice.Value);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Failed: {exception.Message}");
            }
        }
    }

    private static void PrintMenu()
    {
        Console.WriteLine();
        Console.WriteLine("What do you want to do?");
        Console.WriteLine($"  {(int)MenuChoice.CreateMigration}. Create a migration");
        Console.WriteLine($"  {(int)MenuChoice.ApplyAllMigrations}. Apply all migrations");
        Console.WriteLine($"  {(int)MenuChoice.MoveToMigration}. Apply or roll back to a migration");
        Console.WriteLine($"  {(int)MenuChoice.Exit}. Exit");
        Console.Write("Choice: ");
    }

    private static MenuChoice? ReadMenuChoice()
    {
        var answer = Console.ReadLine();

        // The input has ended (for example the console was closed), so there is nothing left to choose.
        if (answer is null)
        {
            return MenuChoice.Exit;
        }

        if (int.TryParse(answer, out var number) && Enum.IsDefined(typeof(MenuChoice), number))
        {
            return (MenuChoice)number;
        }

        return null;
    }

    private Task RunChoiceAsync(MenuChoice choice) => choice switch
    {
        MenuChoice.CreateMigration => CreateMigrationAsync(),
        MenuChoice.ApplyAllMigrations => ApplyAllMigrationsAsync(),
        MenuChoice.MoveToMigration => MoveToMigrationAsync(),
        _ => throw new ArgumentOutOfRangeException(nameof(choice), choice, "There is no action for this choice.")
    };

    private async Task CreateMigrationAsync()
    {
        Console.Write("Name of the new migration (empty to cancel): ");

        var migrationName = Console.ReadLine()?.Trim();

        if (string.IsNullOrEmpty(migrationName))
        {
            Console.WriteLine("No name given. No migration was created.");
            return;
        }

        var modelCheck = await dotnetEfRunner.RunMigrationsCommandAsync(DotnetEfRunner.HasPendingModelChangesCommand);

        if (modelCheck.ExitCode == 0)
        {
            Console.WriteLine("The model has no changes since the last migration. No migration was created.");
            return;
        }

        // The command also ends with an error code when it fails (for example when the build fails).
        if (!modelCheck.Output.Contains(ModelChangedText, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException($"Could not check the model for changes:{Environment.NewLine}{modelCheck.Output}");
        }

        var addResult = await dotnetEfRunner.RunMigrationsCommandAsync(DotnetEfRunner.AddMigrationCommand, migrationName);

        if (addResult.ExitCode != 0)
        {
            throw new InvalidOperationException($"Could not create the migration:{Environment.NewLine}{addResult.Output}");
        }

        Console.WriteLine($"Migration {migrationName} was created.");
    }

    private async Task ApplyAllMigrationsAsync()
    {
        var builtMigrationIds = context.Database.GetMigrations().ToList();
        var fileOnlyMigrationIds = ReadMigrationIdsFromFiles().Except(builtMigrationIds).ToList();

        if (builtMigrationIds.Count == 0 && fileOnlyMigrationIds.Count == 0)
        {
            Console.WriteLine("There are no migrations. Create one first.");
            return;
        }

        var pendingMigrations = (await context.Database.GetPendingMigrationsAsync()).Concat(fileOnlyMigrationIds).ToList();

        if (pendingMigrations.Count == 0)
        {
            Console.WriteLine("The database is up to date.");
            return;
        }

        // The running program cannot apply a migration it was not built with, but dotnet ef builds the project first.
        if (fileOnlyMigrationIds.Count > 0)
        {
            await UpdateDatabaseWithEfAsync(targetMigrationId: null);
        }
        else
        {
            await context.Database.MigrateAsync();
        }

        Console.WriteLine($"Applied {pendingMigrations.Count} migration(s) correctly:");

        foreach (var migrationId in pendingMigrations)
        {
            Console.WriteLine($"  {migrationId}");
        }
    }

    private async Task MoveToMigrationAsync()
    {
        var allMigrationIds = context.Database.GetMigrations().ToList();
        var appliedMigrationIds = (await context.Database.GetAppliedMigrationsAsync()).ToList();
        var listedMigrations = ListMigrations(allMigrationIds, appliedMigrationIds);

        var targetMigration = ChooseMigration(listedMigrations);

        if (targetMigration is null)
        {
            return;
        }

        var targetMigrationId = targetMigration.Id;

        if (targetMigration.Status == MigrationStatus.NotBuiltYet)
        {
            await UpdateDatabaseWithEfAsync(targetMigrationId);

            Console.WriteLine($"Applied up to {targetMigrationId} correctly.");
            return;
        }

        // The empty database is not in the list of built migrations, so its index is -1, the same as a database with
        // nothing applied.
        var targetIndex = allMigrationIds.IndexOf(targetMigrationId);
        var lastAppliedIndex = appliedMigrationIds.Count == 0 ? -1 : allMigrationIds.IndexOf(appliedMigrationIds[^1]);

        if (targetIndex == lastAppliedIndex)
        {
            Console.WriteLine($"The database is already at {Label(targetMigration)}.");
            return;
        }

        var isRollback = targetIndex < lastAppliedIndex;

        if (isRollback)
        {
            var rolledBackCount = appliedMigrationIds.Count(migrationId => allMigrationIds.IndexOf(migrationId) > targetIndex);

            if (!ConfirmRollback(rolledBackCount))
            {
                return;
            }
        }

        await context.GetService<IMigrator>().MigrateAsync(targetMigrationId);

        var action = isRollback ? "Rolled back to" : "Applied up to";
        Console.WriteLine($"{action} {Label(targetMigration)} correctly.");
    }

    /// <summary>
    /// Shows the migrations in pages of <see cref="PageSize"/>, newest page first, until the developer picks one.
    /// Returns null when the developer goes back to the menu.
    /// </summary>
    private static ListedMigration? ChooseMigration(List<ListedMigration> listedMigrations)
    {
        var pageNumber = 0;

        while (true)
        {
            var pageEnd = listedMigrations.Count - pageNumber * PageSize;
            var pageStart = Math.Max(0, pageEnd - PageSize);
            var hasOlderPage = pageStart > 0;
            var hasNewerPage = pageNumber > 0;

            Console.WriteLine();

            for (var position = pageStart; position < pageEnd; position++)
            {
                Console.WriteLine($"  {position - pageStart + 1}. {Describe(listedMigrations[position])}");
            }

            Console.WriteLine($"  {BackToMenuAnswer}. Back to the menu");

            if (hasOlderPage)
            {
                Console.WriteLine($"  {PreviousPageAnswer}. Previous (older migrations)");
            }

            if (hasNewerPage)
            {
                Console.WriteLine($"  {NextPageAnswer}. Next (newer migrations)");
            }

            Console.Write("Number of the migration to go to: ");

            var answer = Console.ReadLine()?.Trim().ToLowerInvariant();

            // The input has ended (for example the console was closed), so there is nothing left to choose.
            if (answer is null || answer == BackToMenuAnswer)
            {
                return null;
            }

            if (answer == PreviousPageAnswer && hasOlderPage)
            {
                pageNumber++;
            }
            else if (answer == NextPageAnswer && hasNewerPage)
            {
                pageNumber--;
            }
            else if (int.TryParse(answer, out var number) && number >= 1 && number <= pageEnd - pageStart)
            {
                return listedMigrations[pageStart + number - 1];
            }
            else
            {
                Console.WriteLine("That is not one of the options.");
            }
        }
    }

    private static string Describe(ListedMigration listedMigration)
    {
        if (listedMigration.IsEmptyDatabase)
        {
            return EmptyDatabaseText;
        }

        TryReadCreated(listedMigration.Id, out var created);

        var name = listedMigration.Id[(listedMigration.Id.IndexOf(MigrationIdNameSeparator) + 1)..];
        var status = listedMigration.Status == MigrationStatus.Applied ? "applied" : "pending";

        return $"{created.ToString(ListedDateFormat, CultureInfo.InvariantCulture)} | {name} | {listedMigration.Id}{MigrationFileExtension} | {status}";
    }

    private static string Label(ListedMigration listedMigration) =>
        listedMigration.IsEmptyDatabase ? EmptyDatabaseText : listedMigration.Id;

    private static bool ConfirmRollback(int rolledBackCount)
    {
        Console.WriteLine($"This undoes {rolledBackCount} migration(s) and can delete data.");
        Console.Write($"Type {ConfirmRollbackAnswer} to continue: ");

        if (string.Equals(Console.ReadLine()?.Trim(), ConfirmRollbackAnswer, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        Console.WriteLine("Nothing was changed.");
        return false;
    }

    private async Task UpdateDatabaseWithEfAsync(string? targetMigrationId)
    {
        var updateResult = await dotnetEfRunner.RunDatabaseUpdateAsync(targetMigrationId);

        if (updateResult.ExitCode != 0)
        {
            throw new InvalidOperationException($"Could not update the database:{Environment.NewLine}{updateResult.Output}");
        }
    }

    // The oldest state comes first: the empty database, then the migrations from old to new.
    private List<ListedMigration> ListMigrations(List<string> allMigrationIds, List<string> appliedMigrationIds)
    {
        var builtMigrations = allMigrationIds
            .Select(migrationId => new ListedMigration(
                migrationId,
                appliedMigrationIds.Contains(migrationId) ? MigrationStatus.Applied : MigrationStatus.Pending));

        var notBuiltMigrations = ReadMigrationIdsFromFiles()
            .Except(allMigrationIds)
            .Select(migrationId => new ListedMigration(migrationId, MigrationStatus.NotBuiltYet));

        var emptyDatabaseStatus = appliedMigrationIds.Count == 0 ? MigrationStatus.Applied : MigrationStatus.Pending;

        return builtMigrations
            .Concat(notBuiltMigrations)
            .OrderBy(listedMigration => listedMigration.Id, StringComparer.Ordinal)
            .Prepend(new ListedMigration(Migration.InitialDatabase, emptyDatabaseStatus))
            .ToList();
    }

    // Migrations created since the program was built are only in the files, not in the running program.
    private IEnumerable<string> ReadMigrationIdsFromFiles()
    {
        var migrationsFolder = Path.Combine(dotnetEfRunner.ResolveDatabaseProjectPath(), MigrationsFolderName);

        if (!Directory.Exists(migrationsFolder))
        {
            return [];
        }

        // The model snapshot is in the same folder, but its name has no timestamp, so it is not a migration.
        return Directory.EnumerateFiles(migrationsFolder, MigrationFilePattern)
            .Select(Path.GetFileNameWithoutExtension)
            .OfType<string>()
            .Where(fileName => !fileName.EndsWith(DesignerFileSuffix, StringComparison.Ordinal))
            .Where(fileName => TryReadCreated(fileName, out _));
    }

    private static bool TryReadCreated(string migrationId, out DateTime created)
    {
        created = default;

        return migrationId.Length >= MigrationIdTimestampFormat.Length
            && DateTime.TryParseExact(
                migrationId[..MigrationIdTimestampFormat.Length],
                MigrationIdTimestampFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal,
                out created);
    }
}
