using System.Security.Cryptography;
using System.Text;

namespace FantasyInformator.Database.Context.Seeds;

/// <summary>
/// The ids and dates of the seed rows. Both are fixed, because a new random id or the current time on every
/// model build would make every new migration show a change in the seed data.
/// </summary>
public static class SeedIds
{
    private const int GuidByteCount = 16;
    private const string DatabaseModelSuffix = "Db";

    public static readonly DateTime SeedDateUtc = new(2026, 11, 8, 0, 0, 0, DateTimeKind.Utc);

    /// <summary>
    /// The same table and key always give the same id. The table is the name of the database model,
    /// such as <c>nameof(SlotDb)</c>; its <c>Db</c> ending is left out, so the ids stay as they were.
    /// </summary>
    public static Guid For(string table, string key) =>
        new(SHA256.HashData(Encoding.UTF8.GetBytes($"{WithoutSuffix(table)}:{key}")).AsSpan(0, GuidByteCount));

    private static string WithoutSuffix(string name) =>
        name.EndsWith(DatabaseModelSuffix, StringComparison.Ordinal) ? name[..^DatabaseModelSuffix.Length] : name;
}
