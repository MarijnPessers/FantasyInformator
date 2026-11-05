using AppModels = FantasyInformator.Models;
using DatabaseModels = FantasyInformator.Database.Models;

namespace FantasyInformator.Database.Mappers;

public static class RosteredPlayerMapper
{
    public static AppModels.RosteredPlayer? ToModel(DatabaseModels.RosteredPlayer? rosteredPlayer) =>
        ToModel(rosteredPlayer, new MappingContext());

    public static AppModels.RosteredPlayer? ToModel(DatabaseModels.RosteredPlayer? rosteredPlayer, MappingContext context)
    {
        if (rosteredPlayer is null)
        {
            return null;
        }

        if (context.TryGetMapped(rosteredPlayer, out AppModels.RosteredPlayer? mapped))
        {
            return mapped;
        }

        mapped = TrackedModelMapper.Fill(rosteredPlayer, new AppModels.RosteredPlayer
        {
            FantasyTeamId = rosteredPlayer.FantasyTeamId,
            PlayerId = rosteredPlayer.PlayerId,
            SlotId = rosteredPlayer.SlotId,
        });
        context.Register(rosteredPlayer, mapped);

        mapped.FantasyTeam = FantasyTeamMapper.ToModel(rosteredPlayer.FantasyTeam, context);
        mapped.Player = PlayerMapper.ToModel(rosteredPlayer.Player, context);
        mapped.Slot = SlotMapper.ToModel(rosteredPlayer.Slot, context);

        return mapped;
    }
}
