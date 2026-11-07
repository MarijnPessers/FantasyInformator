using FantasyInformator.Database.Mappers.Lineup;
using FantasyInformator.Database.Mappers.Tracking;
using AppPlayer = FantasyInformator.Models.Player;
using DatabasePlayer = FantasyInformator.Database.Models.Player;

namespace FantasyInformator.Database.Mappers.Player;

public static class PositionMapper
{
    public static AppPlayer.Position? ToModel(DatabasePlayer.Position? position) =>
        ToModel(position, new MappingContext());

    public static AppPlayer.Position? ToModel(DatabasePlayer.Position? position, MappingContext context)
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

        mapped.PlayerPositions.AddRange(context.MapList(position.PlayerPositions, PlayerPositionMapper.ToModel));
        mapped.SlotEligibilities.AddRange(context.MapList(position.SlotEligibilities, SlotEligibilityMapper.ToModel));

        return mapped;
    }
}
