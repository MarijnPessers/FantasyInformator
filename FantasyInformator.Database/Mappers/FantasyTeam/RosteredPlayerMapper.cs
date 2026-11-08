using FantasyInformator.Database.Mappers.Lineup;
using FantasyInformator.Database.Mappers.Player;
using FantasyInformator.Database.Mappers.Tracking;
using AppFantasyTeam = FantasyInformator.Models.FantasyTeam;
using DatabaseFantasyTeam = FantasyInformator.Database.Models.FantasyTeam;

namespace FantasyInformator.Database.Mappers.FantasyTeam;

public static class RosteredPlayerMapper
{
    public static AppFantasyTeam.RosteredPlayer? Map(
        this DatabaseFantasyTeam.RosteredPlayer? rosteredPlayer) =>
        rosteredPlayer.Map(new MappingContext());

    public static AppFantasyTeam.RosteredPlayer? Map(
        this DatabaseFantasyTeam.RosteredPlayer? rosteredPlayer,
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

        mapped.FantasyTeam = rosteredPlayer.FantasyTeam.Map(context);
        mapped.Player = rosteredPlayer.Player.Map(context);
        mapped.Slot = rosteredPlayer.Slot.Map(context);

        return mapped;
    }

    public static DatabaseFantasyTeam.RosteredPlayer Map(this AppFantasyTeam.RosteredPlayer rosteredPlayer) =>
        TrackedModelMapper.FillDatabase(rosteredPlayer, new DatabaseFantasyTeam.RosteredPlayer
        {
            FantasyTeamId = rosteredPlayer.FantasyTeamId,
            PlayerId = rosteredPlayer.PlayerId,
            SlotId = rosteredPlayer.SlotId,
        });
}
