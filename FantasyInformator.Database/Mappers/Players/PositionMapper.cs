using FantasyInformator.Database.Mappers.Lineups;
using FantasyInformator.Database.Mappers.Tracking;
using FantasyInformator.Database.Models.Players;
using AppPlayer = FantasyInformator.Domain.Players;

namespace FantasyInformator.Database.Mappers.Players;

public static class PositionMapper
{
    public static AppPlayer.Position Map(this PositionDb position) =>
        TrackedModelMapper.MapToAppModel(position, new AppPlayer.Position
        {
            Name = position.Name,
            Abbreviation = position.Abbreviation,
            IsDominant = position.IsDominant,
            PlayerPositions = [.. position.PlayerPositions.Select(x => x.Map())],
            SlotEligibilities = [.. position.SlotEligibilities.Select(x => x.Map())],
        });

    public static PositionDb Map(this AppPlayer.Position position) =>
        TrackedModelMapper.Map(position, new PositionDb
        {
            Name = position.Name,
            Abbreviation = position.Abbreviation,
            IsDominant = position.IsDominant,
            PlayerPositions = [.. position.PlayerPositions.Select(x => x.Map())],
            SlotEligibilities = [.. position.SlotEligibilities.Select(x => x.Map())],
        });
}
