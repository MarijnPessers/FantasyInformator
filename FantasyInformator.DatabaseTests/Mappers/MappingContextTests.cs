using FantasyInformator.Database.Mappers;
using AppModels = FantasyInformator.Models;
using DatabaseModels = FantasyInformator.Database.Models;

namespace FantasyInformator.DatabaseTests.Mappers;

[TestClass]
public class MappingContextTests
{
    [TestMethod]
    public void TryGetMapped_NothingRegistered_ReturnsFalse()
    {
        var context = new MappingContext();

        var found = context.TryGetMapped(new DatabaseModels.League(), out AppModels.League? mapped);

        Assert.IsFalse(found);
        Assert.IsNull(mapped);
    }

    [TestMethod]
    public void TryGetMapped_Registered_ReturnsTheSameObject()
    {
        var context = new MappingContext();
        var databaseLeague = new DatabaseModels.League();
        var league = new AppModels.League();
        context.Register(databaseLeague, league);

        var found = context.TryGetMapped(databaseLeague, out AppModels.League? mapped);

        Assert.IsTrue(found);
        Assert.AreSame(league, mapped);
    }

    [TestMethod]
    public void TryGetMapped_OtherDatabaseObjectWithSameValues_ReturnsFalse()
    {
        var context = new MappingContext();
        context.Register(new DatabaseModels.League(), new AppModels.League());

        var found = context.TryGetMapped(new DatabaseModels.League(), out AppModels.League? _);

        Assert.IsFalse(found);
    }

    [TestMethod]
    public void MapList_MapsEveryElementWithTheContext()
    {
        var context = new MappingContext();
        var databaseLeagues = new List<DatabaseModels.League> { new(), new(), new() };

        var result = context.MapList(databaseLeagues, LeagueMapper.ToModel);

        Assert.HasCount(3, result);
        Assert.IsTrue(context.TryGetMapped(databaseLeagues[1], out AppModels.League? mapped));
        Assert.AreSame(result[1], mapped);
    }

    [TestMethod]
    public void MapList_MapperReturnsNull_SkipsTheElement()
    {
        var context = new MappingContext();
        var databaseLeagues = new List<DatabaseModels.League> { new(), new() };

        var result = context.MapList(databaseLeagues, (_, _) => (AppModels.League?)null);

        Assert.IsEmpty(result);
    }
}
