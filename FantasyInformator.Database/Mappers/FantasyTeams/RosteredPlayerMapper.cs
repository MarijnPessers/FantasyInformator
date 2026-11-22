using FantasyInformator.Database.Mappers.Tracking;
using FantasyInformator.Database.Models.FantasyTeams;
using AppFantasyTeam = FantasyInformator.Domain.FantasyTeams;

namespace FantasyInformator.Database.Mappers.FantasyTeams;

public static class RosteredPlayerMapper
{
    public static AppFantasyTeam.RosteredPlayer Map(this RosteredPlayerDb rosteredPlayer) =>
        TrackedModelMapper.MapToAppModel(rosteredPlayer, new AppFantasyTeam.RosteredPlayer
        {
            FantasyTeamId = rosteredPlayer.FantasyTeamId,
            PlayerId = rosteredPlayer.PlayerId,
            SlotId = rosteredPlayer.SlotId,
        });

    public static RosteredPlayerDb Map(this AppFantasyTeam.RosteredPlayer rosteredPlayer) =>
        TrackedModelMapper.Map(rosteredPlayer, new RosteredPlayerDb
        {
            FantasyTeamId = rosteredPlayer.FantasyTeamId,
            PlayerId = rosteredPlayer.PlayerId,
            SlotId = rosteredPlayer.SlotId,
        });
}
