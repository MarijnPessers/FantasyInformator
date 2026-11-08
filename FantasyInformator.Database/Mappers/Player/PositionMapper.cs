using FantasyInformator.Database.Mappers.Lineup;
using FantasyInformator.Database.Mappers.Tracking;
using AppPlayer = FantasyInformator.Models.Player;
using DatabasePlayer = FantasyInformator.Database.Models.Player;

namespace FantasyInformator.Database.Mappers.Player;

public static class PositionMapper
{
    public static AppPlayer.Position? Map(this DatabasePlayer.Position? position) =>
        position.Map(new MappingContext());

    public static AppPlayer.Position? Map(this DatabasePlayer.Position? position, MappingContext context)
    {
        if (position is null)
        {
            return null;
        }

        if (context.TryGetMapped(position, out AppPlayer.Position? mapped))
        {
            return mapped;
        }

        mapped = TrackedModelMapper.Fill(position, new AppPlayer.Position
        {
            Name = position.Name,
            Abbreviation = position.Abbreviation,
        });
        context.Register(position, mapped);

        mapped.PlayerPositions.AddRange(
            context.MapList(position.PlayerPositions, PlayerPositionMapper.Map));
        mapped.SlotEligibilities.AddRange(
            context.MapList(position.SlotEligibilities, SlotEligibilityMapper.Map));

        return mapped;
    }

    public static DatabasePlayer.Position Map(this AppPlayer.Position position) =>
        TrackedModelMapper.FillDatabase(position, new DatabasePlayer.Position
        {
            Name = position.Name,
            Abbreviation = position.Abbreviation,
        });
}
