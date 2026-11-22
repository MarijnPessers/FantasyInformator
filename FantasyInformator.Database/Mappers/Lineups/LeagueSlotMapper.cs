using FantasyInformator.Database.Models.Lineups;
using FantasyInformator.Domain.Lineups;

namespace FantasyInformator.Database.Mappers.Lineups;

internal sealed class LeagueSlotMapper : Mapper<LeagueSlot, LeagueSlotDb>
{
    protected override LeagueSlot CreateModel(LeagueSlotDb databaseModel) =>
        new()
        {
            LeagueInstanceId = databaseModel.LeagueInstanceId,
            SlotId = databaseModel.SlotId,
            Count = databaseModel.Count,
        };

    protected override LeagueSlotDb CreateDatabaseModel(LeagueSlot model) =>
        new()
        {
            LeagueInstanceId = model.LeagueInstanceId,
            SlotId = model.SlotId,
            Count = model.Count,
        };
}
