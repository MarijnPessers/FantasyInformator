using AutoFixture;
using FantasyInformator.Database.Mappers.Tracking;
using AppEnums = FantasyInformator.Models.Enums;
using AppLeague = FantasyInformator.Models.League;
using DatabaseLeague = FantasyInformator.Database.Models.League;

namespace FantasyInformator.Database.Tests.Mappers.Tracking;

[TestClass]
public class TrackedModelMapperTests
{
    private readonly Fixture _fixture = new();

    [TestMethod]
    public void MapToAppModel_CopiesIdAndReturnsTheModel()
    {
        var databaseLeague = new DatabaseLeague.League
        {
            Id = _fixture.Create<Guid>(),
        };
        var league = new AppLeague.League();

        var result = TrackedModelMapper.MapToAppModel(databaseLeague, league);

        Assert.AreSame(league, result);
        Assert.AreEqual(databaseLeague.Id, result.Id);
    }

    [TestMethod]
    public void MapToAppModel_SetsStateToUnchanged()
    {
        var databaseLeague = new DatabaseLeague.League();
        var league = new AppLeague.League { State = AppEnums.TrackingState.New };

        var result = TrackedModelMapper.MapToAppModel(databaseLeague, league);

        Assert.AreEqual(AppEnums.TrackingState.Unchanged, result.State);
    }
}
