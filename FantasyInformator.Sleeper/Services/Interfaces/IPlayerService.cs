using FantasyInformator.Models.Player;

namespace FantasyInformator.Sleeper.Services.Interfaces;

/// <summary>
/// Retrieves the data of the Player group:
/// <c>Player</c>, <c>Position</c>, <c>PlayerPosition</c> and <c>GameAppearance</c>.
/// A player comes back with his <c>PlayerPosition</c> records filled. Links to other records hold only their key.
/// The Sleeper models stay inside this project.
/// </summary>
public interface IPlayerService
{
    /// <summary>
    /// Gets every NFL player. About 5 MB, and Sleeper allows it once a day at most.
    /// </summary>
    /// <remarks>
    /// <para>Call: <c>GET https://api.sleeper.app/v1/players/nfl</c></para>
    /// <para>Used for:</para>
    /// <list type="bullet">
    /// <item>the key of the response (<c>player_id</c>): <c>Player.ExternalId</c></item>
    /// <item><c>first_name</c> and <c>last_name</c>: <c>Player.Name</c></item>
    /// <item><c>team</c>: <c>Player.CurrentNflTeamId</c>,
    /// through the <c>NflTeam</c> with that abbreviation as <c>ExternalId</c></item>
    /// <item><c>position</c> and <c>fantasy_positions</c>: <c>Position</c> (an unknown position is added)
    /// and <c>PlayerPosition</c></item>
    /// <item><c>injury_status</c>: <c>Player.InjuryStatus</c> (the statuses "IR", "PUP", "Out", "Questionable" and
    /// "Doubtful"; any other status or none gives <c>None</c>)</item>
    /// </list>
    /// <para>Not used: <c>hashtag</c>, <c>depth_chart_position</c>, <c>depth_chart_order</c>, <c>status</c>,
    /// <c>sport</c>, <c>number</c>, <c>search_first_name</c>, <c>search_last_name</c>, <c>search_full_name</c>,
    /// <c>search_rank</c>, <c>injury_start_date</c>, <c>practice_participation</c>,
    /// <c>weight</c>, <c>height</c>, <c>age</c>, <c>college</c>, <c>birth_country</c>, <c>years_exp</c>,
    /// and the ids at other sites (<c>sportradar_id</c>, <c>fantasy_data_id</c>, <c>stats_id</c>, <c>espn_id</c>,
    /// <c>rotowire_id</c>, <c>rotoworld_id</c>, <c>yahoo_id</c>).</para>
    /// </remarks>
    Task<IReadOnlyList<Player>> GetPlayersAsync(CancellationToken cancellationToken);

    /// <inheritdoc cref="GetPlayersAsync(CancellationToken)"/>
    Task<IReadOnlyList<Player>> GetPlayersAsync() =>
        GetPlayersAsync(CancellationToken.None);

    /// <summary>
    /// Gets which players played in which game in one week, for the given positions.
    /// Unofficial call, so it can change or be blocked.
    /// </summary>
    /// <remarks>
    /// <para>Call: <c>GET https://api.sleeper.com/stats/nfl/&lt;season&gt;/&lt;week&gt;</c>
    /// with the query <c>?season_type=regular&amp;position[]=&lt;position&gt;</c>
    /// (<c>position[]</c> can be given more than once)</para>
    /// <para>Used for:</para>
    /// <list type="bullet">
    /// <item><c>player_id</c> and <c>game_id</c>:
    /// <c>GameAppearance.PlayerId</c> and <c>GameAppearance.GameId</c></item>
    /// <item><c>team</c>: <c>GameAppearance.IsHome</c>, true when it is the home team of the game.
    /// It is the team of that game, so a traded player is handled.</item>
    /// </list>
    /// <para>Not used: <c>stats</c> (the points come from the matchups call of <see cref="IScoringService"/>,
    /// scored with the league's scoring), <c>status</c>, <c>date</c>, <c>category</c>, <c>last_modified</c>,
    /// <c>updated_at</c>, <c>week</c>, <c>sport</c>, <c>season_type</c>, <c>season</c>, <c>opponent</c>,
    /// <c>week_shard</c>, <c>company</c>, and the whole <c>player</c> object
    /// (names, positions, team, injury fields, years_exp, news_updated, metadata).</para>
    /// </remarks>
    Task<IReadOnlyList<GameAppearance>> GetWeekStatsAsync(
        int season,
        int week,
        IReadOnlyCollection<string> positions,
        CancellationToken cancellationToken);

    /// <inheritdoc cref="GetWeekStatsAsync(int, int, IReadOnlyCollection{string}, CancellationToken)"/>
    Task<IReadOnlyList<GameAppearance>> GetWeekStatsAsync(
        int season,
        int week,
        IReadOnlyCollection<string> positions) =>
        GetWeekStatsAsync(season, week, positions, CancellationToken.None);
}
