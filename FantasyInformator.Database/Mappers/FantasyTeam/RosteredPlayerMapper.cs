using FantasyInformator.Database.Mappers.Lineup;
using FantasyInformator.Database.Mappers.Player;
using FantasyInformator.Database.Mappers.Tracking;
using AppFantasyTeam = FantasyInformator.Models.FantasyTeam;
using DatabaseFantasyTeam = FantasyInformator.Database.Models.FantasyTeam;

namespace FantasyInformator.Database.Mappers.FantasyTeam;

public static class RosteredPlayerMapper
{
    public static AppFantasyTeam.RosteredPlayer? ToModel(DatabaseFantasyTeam.RosteredPlayer? rosteredPlayer) =>
        ToModel(rosteredPlayer, new MappingContext());

    public static AppFantasyTeam.RosteredPlayer? ToModel(
        DatabaseFantasyTeam.RosteredPlayer? rosteredPlayer,
        MappingContext context)
    {
        if (rosteredPlayer is null)
        {
            return null;
        }

        if (context.TryGetMapped(rosteredPlayer, out AppFantasyTeam.RosteredPlayer? mapped))
        {
            return mapped;
        }

        mapped = TrackedModelMapper.Fill(rosteredPlayer, new AppFantasyTeam.RosteredPlayer
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
