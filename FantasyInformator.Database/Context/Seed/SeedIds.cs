using System.Security.Cryptography;
using System.Text;

namespace FantasyInformator.Database.Context.Seed;

/// <summary>
/// The ids and dates of the seed rows. Both are fixed, because a new random id or the current time on every
/// model build would make every new migration show a change in the seed data.
/// </summary>
public static class SeedIds
{
    private const int GuidByteCount = 16;

    public static readonly DateTime SeedDateUtc = new(2026, 11, 8, 0, 0, 0, DateTimeKind.Utc);

    /// <summary>The same table and key always give the same id.</summary>
    public static Guid For(string table, string key) =>
        new(SHA256.HashData(Encoding.UTF8.GetBytes($"{table}:{key}")).AsSpan(0, GuidByteCount));
}
