using AutoFixture;
using FantasyInformator.Database.Mappers.Tracking;
using AppEnums = FantasyInformator.Domain.Enums;
using AppLeague = FantasyInformator.Domain.League;
using DatabaseLeague = FantasyInformator.Database.Models.League;

namespace FantasyInformator.Database.Tests.Mappers.Tracking;

[TestClass]
public class TrackedModelMapperTests
{
    private readonly Fixture _fixture = new();

    [TestMethod]
    public void MapToAppModel_ShouldCopyIdAndReturnTheModel_WhenDatabaseModelIsGiven()
    {
        var databaseLeague = new DatabaseLeague.League
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
        var databaseLeague = new DatabaseLeague.League { Name = _fixture.Create<string>() };
        var league = new AppLeague.League { Name = _fixture.Create<string>(), State = AppEnums.TrackingState.New };

        var result = TrackedModelMapper.MapToAppModel(databaseLeague, league);

        Assert.AreEqual(AppEnums.TrackingState.Unchanged, result.State);
    }
}
