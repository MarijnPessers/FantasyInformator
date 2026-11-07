using System.Globalization;
using FantasyInformator.Models.League;
using FantasyInformator.Models.Lineup;
using FantasyInformator.Models.Nfl;
using SleeperModels = FantasyInformator.Sleeper.Services.Models;

namespace FantasyInformator.Sleeper.Mappers;

/// <summary>
/// Maps the Sleeper league to the League group and the Lineup counts of that league.
/// Links to other records are stubs that hold only the key (<c>ExternalId</c>, or <c>Year</c> for a season).
/// </summary>
public static class LeagueMapper
{
    public static League ToLeague(SleeperModels.League league) =>
        new()
        {
            Name = league.Name,
        };

    /// <summary>Throws when the season of the league is not a number.</summary>
    public static LeagueInstance ToLeagueInstance(SleeperModels.League league) =>
        new()
        {
            ExternalId = league.LeagueId,
            Season = new Season { Year = int.Parse(league.Season, CultureInfo.InvariantCulture) },
        };

    /// <summary>
    /// Counts how often each slot occurs in <c>roster_positions</c>, and adds IR and taxi from the settings.
    /// A slot the league does not have is not returned.
    /// </summary>
    public static List<LeagueSlot> ToLeagueSlots(SleeperModels.League league)
    {
        List<(string Code, int Count)> slotCounts = [.. league.RosterPositions
            .GroupBy(s => s)
            .Select(g => (Code: g.Key, Count: g.Count()))];

        if (league.Settings.ReserveSlots > 0)
        {
            slotCounts.Add((SlotCodes.InjuredReserve, league.Settings.ReserveSlots));
        }

        if (league.Settings.TaxiSlots > 0)
        {
            slotCounts.Add((SlotCodes.Taxi, league.Settings.TaxiSlots));
        }

        return [.. slotCounts
            .Select(x => new LeagueSlot
            {
                Slot = new Slot { ExternalId = x.Code },
                Count = x.Count,
            })];
    }
}
