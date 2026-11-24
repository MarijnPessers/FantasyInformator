using FantasyInformator.Database.Models.Lineups;
using FantasyInformator.Domain.Lineups;

namespace FantasyInformator.Database.Mappers.Lineups;

internal sealed class LeagueSlotMapper : EntityMapper<LeagueSlot, LeagueSlotDb>
{
    protected override LeagueSlot CreateCopy(LeagueSlotDb source) =>
        new()
        {
            LeagueInstanceId = source.LeagueInstanceId,
            SlotId = source.SlotId,
            Count = source.Count,
        };

    protected override LeagueSlotDb CreateCopy(LeagueSlot source) =>
        new()
        {
            LeagueInstanceId = source.LeagueInstanceId,
            SlotId = source.SlotId,
            Count = source.Count,
        };
}
