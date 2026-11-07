using System.Data.Common;
using System.Globalization;
using FantasyInformator.Database;
using FantasyInformator.Migrator.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;

namespace FantasyInformator.Migrator;

/// <summary>
/// The console menu: create a migration, apply all migrations, or move the database to a chosen migration.
/// </summary>
public class MigratorMenu(FantasyInformatorDbContext context, DotnetEfRunner dotnetEfRunner)
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
    private const string PreviousPageAnswer = "P";
    private const string NextPageAnswer = "N";

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
        public bool IsEmptyDatabase =>
            Id == Migration.InitialDatabase;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            PrintMenu();

            var choice = ReadMenuChoice();

            if (choice is null)
            {
                Console.WriteLine(Texts.OptionNotValid);
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
            catch (InvalidOperationException exception)
            {
                PrintFailure(exception);
            }
            catch (IOException exception)
            {
                PrintFailure(exception);
            }
            catch (DbException exception)
            {
                PrintFailure(exception);
            }
            catch (RetryLimitExceededException exception)
            {
                PrintFailure(exception);
            }
        }
    }

    private static void PrintFailure(Exception exception) =>
        Console.WriteLine(Format(Texts.Failed, exception.Message));

    private static string Format(string text, params object[] arguments) =>
        string.Format(CultureInfo.CurrentCulture, text, arguments);

    private static void PrintMenu()
    {
        Console.WriteLine();
        Console.WriteLine(Texts.MenuQuestion);
        Console.WriteLine(Format(Texts.MenuCreateMigration, (int)MenuChoice.CreateMigration));
        Console.WriteLine(Format(Texts.MenuApplyAllMigrations, (int)MenuChoice.ApplyAllMigrations));
        Console.WriteLine(Format(Texts.MenuMoveToMigration, (int)MenuChoice.MoveToMigration));
        Console.WriteLine(Format(Texts.MenuExit, (int)MenuChoice.Exit));
        Console.Write(Texts.MenuPrompt);
    }

    private static MenuChoice? ReadMenuChoice()
    {
        var answer = Console.ReadLine();

        // A null answer means the input has ended (for example the console was closed), so there is nothing left to
        // choose.
        return answer switch
        {
            null => MenuChoice.Exit,
            _ when int.TryParse(answer, out var number) && Enum.IsDefined((MenuChoice)number) => (MenuChoice)number,
            _ => null
        };
    }

    private Task RunChoiceAsync(MenuChoice choice) =>
        choice switch
        {
            MenuChoice.CreateMigration => CreateMigrationAsync(),
            MenuChoice.ApplyAllMigrations => ApplyAllMigrationsAsync(),
            MenuChoice.MoveToMigration => MoveToMigrationAsync(),
            MenuChoice.Exit => throw new NotImplementedException(),
            _ => throw new ArgumentOutOfRangeException(nameof(choice), choice, Texts.NoChoiceAction)
        };

    private async Task CreateMigrationAsync()
    {
        Console.Write(Texts.MigrationNamePrompt);

        var migrationName = Console.ReadLine()?.Trim();

        if (string.IsNullOrEmpty(migrationName))
        {
            Console.WriteLine(Texts.NoNameGiven);
            return;
        }

        var modelCheck = await dotnetEfRunner.RunMigrationsCommandAsync(DotnetEfRunner.HasPendingModelChangesCommand);

        if (modelCheck.ExitCode == 0)
        {
            Console.WriteLine(Texts.ModelHasNoChanges);
            return;
        }

        // The command also ends with an error code when it fails (for example when the build fails).
        if (!modelCheck.Output.Contains(ModelChangedText, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException(
                Format(Texts.ModelCheckFailed, Environment.NewLine, modelCheck.Output));
        }

        var addResult = await dotnetEfRunner.RunMigrationsCommandAsync(
            DotnetEfRunner.AddMigrationCommand,
            migrationName);

        if (addResult.ExitCode != 0)
        {
            throw new InvalidOperationException(
                Format(Texts.MigrationCreateFailed, Environment.NewLine, addResult.Output));
        }

        Console.WriteLine(Format(Texts.MigrationCreated, migrationName));
    }

    private async Task ApplyAllMigrationsAsync()
    {
        List<string> builtMigrationIds = [.. context.Database.GetMigrations()];
        List<string> fileOnlyMigrationIds = [.. ReadMigrationIdsFromFiles().Except(builtMigrationIds)];

        if (builtMigrationIds.Count == 0 && fileOnlyMigrationIds.Count == 0)
        {
            Console.WriteLine(Texts.NoMigrations);
            return;
        }

        List<string> pendingMigrations =
            [.. await context.Database.GetPendingMigrationsAsync(), .. fileOnlyMigrationIds];

        if (pendingMigrations.Count == 0)
        {
            Console.WriteLine(Texts.DatabaseUpToDate);
            return;
        }

        // The running program cannot apply a migration it was not built with, but dotnet ef builds the project first.
        if (fileOnlyMigrationIds.Count > 0)
        {
            await UpdateDatabaseWithEfAsync(null);
        }
        else
        {
            await context.Database.MigrateAsync();
        }

        Console.WriteLine(Format(Texts.AppliedMigrationCount, pendingMigrations.Count));

        foreach (var migrationId in pendingMigrations)
        {
            Console.WriteLine(Format(Texts.AppliedMigrationLine, migrationId));
        }
    }

    private async Task MoveToMigrationAsync()
    {
        List<string> allMigrationIds = [.. context.Database.GetMigrations()];
        List<string> appliedMigrationIds = [.. await context.Database.GetAppliedMigrationsAsync()];
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

            Console.WriteLine(Format(Texts.AppliedUpTo, targetMigrationId));
            return;
        }

        // The empty database is not in the list of built migrations, so its index is -1, the same as a database with
        // nothing applied.
        var targetIndex = allMigrationIds.IndexOf(targetMigrationId);
        var lastAppliedIndex = appliedMigrationIds.Count == 0
            ? -1
            : allMigrationIds.IndexOf(appliedMigrationIds[^1]);

        if (targetIndex == lastAppliedIndex)
        {
            Console.WriteLine(Format(Texts.AlreadyAt, Label(targetMigration)));
            return;
        }

        var isRollback = targetIndex < lastAppliedIndex;

        if (isRollback)
        {
            var rolledBackCount = appliedMigrationIds.Count(x => allMigrationIds.IndexOf(x) > targetIndex);

            if (!ConfirmRollback(rolledBackCount))
            {
                return;
            }
        }

        await context.GetService<IMigrator>().MigrateAsync(targetMigrationId);

        var doneText = isRollback
            ? Texts.RolledBackTo
            : Texts.AppliedUpTo;
        Console.WriteLine(Format(doneText, Label(targetMigration)));
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
            var pageEnd = listedMigrations.Count - (pageNumber * PageSize);
            var pageStart = Math.Max(0, pageEnd - PageSize);
            var hasOlderPage = pageStart > 0;
            var hasNewerPage = pageNumber > 0;

            Console.WriteLine();

            for (var position = pageStart; position < pageEnd; position++)
            {
                var itemNumber = position - pageStart + 1;

                Console.WriteLine(Format(Texts.PageLine, itemNumber, Describe(listedMigrations[position])));
            }

            Console.WriteLine(Format(Texts.BackToMenu, BackToMenuAnswer));

            if (hasOlderPage)
            {
                Console.WriteLine(Format(Texts.PreviousPage, PreviousPageAnswer));
            }

            if (hasNewerPage)
            {
                Console.WriteLine(Format(Texts.NextPage, NextPageAnswer));
            }

            Console.Write(Texts.MigrationNumberPrompt);

            var answer = Console.ReadLine()?.Trim().ToUpperInvariant();

            // The input has ended (for example the console was closed), so there is nothing left to choose.
            if (answer is null or BackToMenuAnswer)
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
                Console.WriteLine(Texts.OptionNotValid);
            }
        }
    }

    private static string Describe(ListedMigration listedMigration)
    {
        if (listedMigration.IsEmptyDatabase)
        {
            return Texts.EmptyDatabase;
        }

        _ = TryReadCreated(listedMigration.Id, out var created);

        var separatorIndex = listedMigration.Id.IndexOf(MigrationIdNameSeparator, StringComparison.Ordinal);
        var name = listedMigration.Id[(separatorIndex + 1)..];
        var status = listedMigration.Status == MigrationStatus.Applied
            ? Texts.StatusApplied
            : Texts.StatusPending;
        var createdText = created.ToString(ListedDateFormat, CultureInfo.InvariantCulture);

        return Format(
            Texts.MigrationDescription,
            createdText,
            name,
            listedMigration.Id + MigrationFileExtension,
            status);
    }

    private static string Label(ListedMigration listedMigration) =>
        listedMigration.IsEmptyDatabase
            ? Texts.EmptyDatabase
            : listedMigration.Id;

    private static bool ConfirmRollback(int rolledBackCount)
    {
        Console.WriteLine(Format(Texts.RollbackWarning, rolledBackCount));
        Console.Write(Format(Texts.RollbackConfirmPrompt, ConfirmRollbackAnswer));

        if (string.Equals(Console.ReadLine()?.Trim(), ConfirmRollbackAnswer, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        Console.WriteLine(Texts.NothingChanged);
        return false;
    }

    private async Task UpdateDatabaseWithEfAsync(string? targetMigrationId)
    {
        var updateResult = await dotnetEfRunner.RunDatabaseUpdateAsync(targetMigrationId);

        if (updateResult.ExitCode != 0)
        {
            throw new InvalidOperationException(
                Format(Texts.DatabaseUpdateFailed, Environment.NewLine, updateResult.Output));
        }
    }

    // The oldest state comes first: the empty database, then the migrations from old to new.
    private List<ListedMigration> ListMigrations(List<string> allMigrationIds, List<string> appliedMigrationIds)
    {
        var builtMigrations = allMigrationIds
            .Select(x => new ListedMigration(x, GetStatus(x, appliedMigrationIds)));

        var notBuiltMigrations = ReadMigrationIdsFromFiles()
            .Except(allMigrationIds)
            .Select(x => new ListedMigration(x, MigrationStatus.NotBuiltYet));

        var emptyDatabaseStatus = appliedMigrationIds.Count == 0
            ? MigrationStatus.Applied
            : MigrationStatus.Pending;

        return [.. builtMigrations
            .Concat(notBuiltMigrations)
            .OrderBy(x => x.Id, StringComparer.Ordinal)
            .Prepend(new ListedMigration(Migration.InitialDatabase, emptyDatabaseStatus))];
    }

    private static MigrationStatus GetStatus(string migrationId, List<string> appliedMigrationIds) =>
        appliedMigrationIds.Contains(migrationId)
            ? MigrationStatus.Applied
            : MigrationStatus.Pending;

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
            .Where(s => !s.EndsWith(DesignerFileSuffix, StringComparison.Ordinal))
            .Where(s => TryReadCreated(s, out _));
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
