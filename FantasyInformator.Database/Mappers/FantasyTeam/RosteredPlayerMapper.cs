using FantasyInformator.Database.Mappers.Tracking;
using AppFantasyTeam = FantasyInformator.Models.FantasyTeam;
using DatabaseFantasyTeam = FantasyInformator.Database.Models.FantasyTeam;

namespace FantasyInformator.Database.Mappers.FantasyTeam;

public static class RosteredPlayerMapper
{
    public static AppFantasyTeam.RosteredPlayer Map(this DatabaseFantasyTeam.RosteredPlayer rosteredPlayer) =>
        TrackedModelMapper.MapToAppModel(rosteredPlayer, new AppFantasyTeam.RosteredPlayer
        {
            FantasyTeamId = rosteredPlayer.FantasyTeamId,
            PlayerId = rosteredPlayer.PlayerId,
            SlotId = rosteredPlayer.SlotId,
        });

    public static DatabaseFantasyTeam.RosteredPlayer Map(this AppFantasyTeam.RosteredPlayer rosteredPlayer) =>
        TrackedModelMapper.MapToDbModel(rosteredPlayer, new DatabaseFantasyTeam.RosteredPlayer
        {
            FantasyTeamId = rosteredPlayer.FantasyTeamId,
            PlayerId = rosteredPlayer.PlayerId,
            SlotId = rosteredPlayer.SlotId,
        });
}
