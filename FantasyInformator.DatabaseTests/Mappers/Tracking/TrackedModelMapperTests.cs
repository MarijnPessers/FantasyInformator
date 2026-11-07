using AutoFixture;
using FantasyInformator.Database.Mappers.Tracking;
using AppEnums = FantasyInformator.Models.Enums;
using AppLeague = FantasyInformator.Models.League;
using DatabaseLeague = FantasyInformator.Database.Models.League;
using DatabaseTracking = FantasyInformator.Database.Models.Tracking;

namespace FantasyInformator.DatabaseTests.Mappers.Tracking;

[TestClass]
public class TrackedModelMapperTests
{
    private readonly Fixture _fixture = new();

    [TestMethod]
    public void Fill_CopiesIdAndUpdatedUtcAndReturnsTheModel()
    {
        var databaseLeague = new DatabaseLeague.League
        {
            Id = _fixture.Create<Guid>(),
            UpdatedUtc = _fixture.Create<DateTime>(),
        };
        var league = new AppLeague.League();

        var result = TrackedModelMapper.Fill(databaseLeague, league);

        Assert.AreSame(league, result);
        Assert.AreEqual(databaseLeague.Id, result.Id);
        Assert.AreEqual(databaseLeague.UpdatedUtc, result.UpdatedUtc);
    }

    [TestMethod]
    [DataRow(DatabaseTracking.TrackingState.New, AppEnums.TrackingState.New)]
    [DataRow(DatabaseTracking.TrackingState.Pending, AppEnums.TrackingState.Pending)]
    [DataRow(DatabaseTracking.TrackingState.Unchanged, AppEnums.TrackingState.Unchanged)]
    [DataRow(DatabaseTracking.TrackingState.Deleted, AppEnums.TrackingState.Deleted)]
    public void Fill_MapsStateByName(DatabaseTracking.TrackingState databaseState, AppEnums.TrackingState expectedState)
    {
        var databaseLeague = new DatabaseLeague.League { State = databaseState };

        var result = TrackedModelMapper.Fill(databaseLeague, new AppLeague.League());

        Assert.AreEqual(expectedState, result.State);
    }
}
