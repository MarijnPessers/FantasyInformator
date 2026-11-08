using FantasyInformator.Database.Mappers.Nfl;
using DatabaseNfl = FantasyInformator.Database.Models.Nfl;

namespace FantasyInformator.Database.Tests.Mappers.Nfl;

[TestClass]
public class SeasonMapperTests
{
    [TestMethod]
    public void Map_MapsAllFieldsAndChildren()
    {
        var season = SourceFactory.Create<DatabaseNfl.Season>();

        var result = SeasonMapper.Map(season);

        Assert.IsNotNull(result);
        MappedAssert.Equal(season, result);
    }

    [TestMethod]
    public void Map_ApplicationModel_MapsAllFieldsAndChildren()
    {
        var model = SourceFactory.Create<DatabaseNfl.Season>().Map();

        var result = model.Map();

        MappedAssert.Equal(result, model);
    }
}
