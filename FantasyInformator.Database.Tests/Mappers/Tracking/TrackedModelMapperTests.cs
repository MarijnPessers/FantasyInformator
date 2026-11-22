using AutoFixture;
using FantasyInformator.Database.Mappers.Tracking;
using FantasyInformator.Database.Models.Leagues;
using AppEnums = FantasyInformator.Domain.Enums;
using AppLeague = FantasyInformator.Domain.Leagues;

namespace FantasyInformator.Database.Tests.Mappers.Tracking;

[TestClass]
public class TrackedModelMapperTests
{
    private readonly Fixture _fixture = new();

    [TestMethod]
    public void MapToAppModel_ShouldCopyIdAndReturnTheModel_WhenDatabaseModelIsGiven()
    {
        var databaseLeague = new LeagueDb
        {
            Id = _fixture.Create<Guid>(),
            Name = _fixture.Create<string>(),
        };
        var league = new AppLeague.League { Name = _fixture.Create<string>() };

        var result = TrackedModelMapper.MapToAppModel(databaseLeague, league);

        Assert.AreSame(league, result);
        Assert.AreEqual(databaseLeague.Id, result.Id);
    }

    [TestMethod]
    public void MapToAppModel_ShouldSetStateToUnchanged_WhenModelIsNew()
    {
        var databaseLeague = new LeagueDb { Name = _fixture.Create<string>() };
        var league = new AppLeague.League { Name = _fixture.Create<string>(), State = AppEnums.TrackingState.New };

        var result = TrackedModelMapper.MapToAppModel(databaseLeague, league);

        Assert.AreEqual(AppEnums.TrackingState.Unchanged, result.State);
    }
}
