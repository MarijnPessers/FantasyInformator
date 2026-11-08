using FantasyInformator.Database.Mappers;
using FantasyInformator.Database.Mappers.League;
using AppLeague = FantasyInformator.Models.League;
using DatabaseLeague = FantasyInformator.Database.Models.League;

namespace FantasyInformator.DatabaseTests.Mappers;

[TestClass]
public class MappingContextTests
{
    [TestMethod]
    public void TryGetMapped_NothingRegistered_ReturnsFalse()
    {
        var context = new MappingContext();

        var found = context.TryGetMapped(new DatabaseLeague.League(), out AppLeague.League? mapped);

        Assert.IsFalse(found);
        Assert.IsNull(mapped);
    }

    [TestMethod]
    public void TryGetMapped_Registered_ReturnsTheSameObject()
    {
        var context = new MappingContext();
        var databaseLeague = new DatabaseLeague.League();
        var league = new AppLeague.League();
        context.Register(databaseLeague, league);

        var found = context.TryGetMapped(databaseLeague, out AppLeague.League? mapped);

        Assert.IsTrue(found);
        Assert.AreSame(league, mapped);
    }

    [TestMethod]
    public void TryGetMapped_OtherDatabaseObjectWithSameValues_ReturnsFalse()
    {
        var context = new MappingContext();
        context.Register(new DatabaseLeague.League(), new AppLeague.League());

        var found = context.TryGetMapped(new DatabaseLeague.League(), out AppLeague.League? _);

        Assert.IsFalse(found);
    }

    [TestMethod]
    public void MapList_MapsEveryElementWithTheContext()
    {
        var context = new MappingContext();
        var databaseLeagues = new List<DatabaseLeague.League> { new(), new(), new() };

        var result = context.MapList(databaseLeagues, LeagueMapper.Map);

        Assert.HasCount(3, result);
        Assert.IsTrue(context.TryGetMapped(databaseLeagues[1], out AppLeague.League? mapped));
        Assert.AreSame(result[1], mapped);
    }

    [TestMethod]
    public void MapList_MapperReturnsNull_SkipsTheElement()
    {
        var context = new MappingContext();
        var databaseLeagues = new List<DatabaseLeague.League> { new(), new() };

        var result = context.MapList(databaseLeagues, (_, _) => (AppLeague.League?)null);

        Assert.IsEmpty(result);
    }
}
