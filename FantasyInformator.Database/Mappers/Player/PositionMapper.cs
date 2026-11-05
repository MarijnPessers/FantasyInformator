using AppModels = FantasyInformator.Models;
using DatabaseModels = FantasyInformator.Database.Models;

namespace FantasyInformator.Database.Mappers;

public static class PositionMapper
{
    public static AppModels.Position? ToModel(DatabaseModels.Position? position) => ToModel(position, new MappingContext());

    public static AppModels.Position? ToModel(DatabaseModels.Position? position, MappingContext context)
    {
        if (position is null)
        {
            return null;
        }

        if (context.TryGetMapped(position, out AppModels.Position? mapped))
        {
            return mapped;
        }

        mapped = TrackedModelMapper.Fill(position, new AppModels.Position
        {
            Name = position.Name,
            Abbreviation = position.Abbreviation,
        });
        context.Register(position, mapped);

        mapped.PlayerPositions = context.MapList(position.PlayerPositions, PlayerPositionMapper.ToModel);
        mapped.SlotEligibilities = context.MapList(position.SlotEligibilities, SlotEligibilityMapper.ToModel);

        return mapped;
    }
}
