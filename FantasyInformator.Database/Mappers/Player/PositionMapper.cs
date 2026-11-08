using FantasyInformator.Database.Mappers.Lineup;
using FantasyInformator.Database.Mappers.Tracking;
using AppPlayer = FantasyInformator.Models.Player;
using DatabasePlayer = FantasyInformator.Database.Models.Player;

namespace FantasyInformator.Database.Mappers.Player;

public static class PositionMapper
{
    public static AppPlayer.Position Map(this DatabasePlayer.Position position) =>
        TrackedModelMapper.MapToAppModel(position, new AppPlayer.Position
        {
            Name = position.Name,
            Abbreviation = position.Abbreviation,
            PlayerPositions = [.. position.PlayerPositions.Select(x => x.Map())],
            SlotEligibilities = [.. position.SlotEligibilities.Select(x => x.Map())],
        });

    public static DatabasePlayer.Position Map(this AppPlayer.Position position) =>
        TrackedModelMapper.MapToDbModel(position, new DatabasePlayer.Position
        {
            Name = position.Name,
            Abbreviation = position.Abbreviation,
            PlayerPositions = [.. position.PlayerPositions.Select(x => x.Map())],
            SlotEligibilities = [.. position.SlotEligibilities.Select(x => x.Map())],
        });
}
