using AutoFixture;
using FantasyInformator.Database.Mappers;
using AppModels = FantasyInformator.Models;
using DatabaseModels = FantasyInformator.Database.Models;

namespace FantasyInformator.DatabaseTests.Mappers;

[TestClass]
public class TrackedModelMapperTests
{
    private readonly Fixture _fixture = new();

    [TestMethod]
    public void Fill_CopiesIdAndUpdatedUtcAndReturnsTheModel()
    {
        var databaseLeague = new DatabaseModels.League
        {
            Id = _fixture.Create<Guid>(),
            UpdatedUtc = _fixture.Create<DateTime>(),
        };
        var league = new AppModels.League();

        var result = TrackedModelMapper.Fill(databaseLeague, league);

        Assert.AreSame(league, result);
        Assert.AreEqual(databaseLeague.Id, result.Id);
        Assert.AreEqual(databaseLeague.UpdatedUtc, result.UpdatedUtc);
    }

    [TestMethod]
    [DataRow(DatabaseModels.TrackingState.New, AppModels.TrackingState.New)]
    [DataRow(DatabaseModels.TrackingState.Pending, AppModels.TrackingState.Pending)]
    [DataRow(DatabaseModels.TrackingState.Unchanged, AppModels.TrackingState.Unchanged)]
    [DataRow(DatabaseModels.TrackingState.Deleted, AppModels.TrackingState.Deleted)]
    public void Fill_MapsStateByName(DatabaseModels.TrackingState databaseState, AppModels.TrackingState expectedState)
    {
        var databaseLeague = new DatabaseModels.League { State = databaseState };

        var result = TrackedModelMapper.Fill(databaseLeague, new AppModels.League());

        Assert.AreEqual(expectedState, result.State);
    }
}
