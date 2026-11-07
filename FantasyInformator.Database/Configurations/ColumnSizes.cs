namespace FantasyInformator.Database.Configurations;

/// <summary>
/// The sizes of the columns, so the tables stay the same wherever they are used.
/// A column without a size would become nvarchar(max), which cannot be indexed.
/// </summary>
public static class ColumnSizes
{
    public const int ExternalId = 64;

    public const int Name = 200;

    public const int Abbreviation = 16;

    public const int CallName = 100;

    /// <summary>
    /// Digits in total of a score. Fantasy points have two decimals; averages and the median get four.
    /// </summary>
    public const int ScorePrecision = 10;

    public const int ScoreScale = 4;
}
