using FantasyInformator.Database.Mappers.Nfl;
using FantasyInformator.Database.Models.Nfl;

namespace FantasyInformator.Database.Tests.Mappers.Nfl;

[TestClass]
public class SeasonMapperTests
{
    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenDatabaseModelIsGiven()
    {
        var season = SourceFactory.Create<SeasonDb>();

        var result = SeasonMapper.Map(season);

        Assert.IsNotNull(result);
        MappedAssert.Equal(season, result);
    }

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenApplicationModelIsGiven()
    {
        var model = SourceFactory.Create<SeasonDb>().Map();

        var result = model.Map();

        MappedAssert.Equal(result, model);
    }
}
