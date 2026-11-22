using FantasyInformator.Database.Models.Nfl;
using FantasyInformator.Domain.Nfl;

namespace FantasyInformator.Database.Mappers.Nfl;

internal static class GameMapper
{
    #region GameDb to Game

    /// <summary>Turns a GameDb into a Game. A new context fills in the links.</summary>
    public static Game Map(this GameDb game) =>
        game.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running, so objects met twice are found.</summary>
    public static Game Map(this GameDb game, MapContext context) =>
        context.Map(
            game,
            x => new Game
            {
                ExternalId = x.ExternalId,
                SeasonId = x.SeasonId,
                Week = x.Week,
                HomeNflTeamId = x.HomeNflTeamId,
                AwayNflTeamId = x.AwayNflTeamId,
                KickoffUtc = x.KickoffUtc,
            }.MapBase(x));

    /// <summary>Turns a list of GameDb into Game, all sharing one context.</summary>
    public static IEnumerable<Game> Map(this IEnumerable<GameDb> items) =>
        items.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running.</summary>
    public static IEnumerable<Game> Map(this IEnumerable<GameDb> items, MapContext context) =>
        [.. items.Select(x => x.Map(context))];

    #endregion

    #region Game to GameDb

    /// <summary>Turns a Game into a GameDb. A new context fills in the links.</summary>
    public static GameDb Map(this Game game) =>
        game.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running, so objects met twice are found.</summary>
    public static GameDb Map(this Game game, MapContext context) =>
        context.Map(
            game,
            x => new GameDb
            {
                ExternalId = x.ExternalId,
                SeasonId = x.SeasonId,
                Week = x.Week,
                HomeNflTeamId = x.HomeNflTeamId,
                AwayNflTeamId = x.AwayNflTeamId,
                KickoffUtc = x.KickoffUtc,
            }.MapBase(x));

    /// <summary>Turns a list of Game into GameDb, all sharing one context.</summary>
    public static IEnumerable<GameDb> Map(this IEnumerable<Game> items) =>
        items.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running.</summary>
    public static IEnumerable<GameDb> Map(this IEnumerable<Game> items, MapContext context) =>
        [.. items.Select(x => x.Map(context))];

    #endregion
}
